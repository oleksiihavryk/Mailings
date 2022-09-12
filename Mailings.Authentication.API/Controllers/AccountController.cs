using IdentityServer4.Services;
using Mailings.Authentication.API.ViewModels;
using Mailings.Authentication.Shared;
using Mailings.Authentication.Shared.ClaimProvider;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mailings.Authentication.API.Exceptions;

namespace Mailings.Authentication.API.Controllers;
[Route("[controller]")]
public sealed class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IIdentityServerInteractionService _interactionService;
    private readonly IClaimProvider<User> _claimProvider;

    public AccountController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IIdentityServerInteractionService interactionService, 
        IClaimProvider<User> claimProvider)
    {
        (_userManager, _signInManager, _interactionService, _claimProvider) =
            (userManager, signInManager, interactionService, claimProvider);
    }

    [HttpGet("[action]")]
    public async Task<ViewResult> Login([FromQuery]string returnUrl) => 
        await Task.Run(() => View((object)returnUrl));

    [HttpGet("[action]")]
    public async Task<ViewResult> SignIn([FromQuery] string returnUrl)
        => await Task.Run(() =>
        {
            var viewModel = new LoginViewModel()
            {
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        });

    [HttpGet("[action]")]
    public async Task<ViewResult> Register([FromQuery] string returnUrl)
        => await Task.Run(() =>
        {
            var viewModel = new RegisterViewModel()
            {
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        });
    [HttpGet("[action]")]
    public async Task<RedirectResult> Logout([FromQuery] string logoutId)
    {
        await _signInManager.SignOutAsync();
        var context =await _interactionService.GetLogoutContextAsync(logoutId);
        return Redirect(context.PostLogoutRedirectUri);
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> SignIn([FromForm]LoginViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        if ((await _userManager.FindByNameAsync(viewModel.Username)) == null)
            return UserNotFound(viewModel);

        await _signInManager.SignOutAsync();

        var signInResult = await _signInManager.PasswordSignInAsync(
            userName: viewModel.Username,
            password: viewModel.Password,
            isPersistent: false,
            lockoutOnFailure: false);

        if (!signInResult.Succeeded)
            return UserNotFound(viewModel);

        return Redirect(viewModel.ReturnUrl);
    }
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Register(RegisterViewModel viewModel)
    {
        if (viewModel.Password != viewModel.PasswordConfirmation)
        {
            ModelState.AddModelError(
                key: string.Empty,
                errorMessage: "Password and confirmation of password is not equals");
        }

        if (await _userManager.FindByEmailAsync(viewModel.Email) != null)
        {
            ModelState.AddModelError(
                key: string.Empty,
                errorMessage: "User with current email is already exist in system.");
        }

        if (await _userManager.FindByNameAsync(viewModel.Username) != null)
        {
            ModelState.AddModelError(
                key: string.Empty,
                errorMessage: "User with current username is already exist in system.");
        }

        if (!ModelState.IsValid)
            return View(viewModel);

        User? user = null;
        try
        {
            user = await CreateUserAsync(viewModel);
            var result = await _signInManager.PasswordSignInAsync(
                user: user,
                password: viewModel.Password,
                isPersistent: false,
                lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new FailedSignInException(user, result);
            }

            return Redirect(viewModel.ReturnUrl);
        }
        catch (FailedSignInException)
        {
            await _userManager.DeleteAsync(user ?? new User());
            throw;
        }
        catch (FailedSignUpException)
        {
            await _userManager.DeleteAsync(user ?? new User());
            throw;
        }
    }

    private async Task<User> CreateUserAsync(RegisterViewModel viewModel)
    {
        const Roles role = Roles.Default;

        User user = new User()
        {
            UserName = viewModel.Username,
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            Email = viewModel.Email,
        };

        var creatingResult = await _userManager.CreateAsync(user, viewModel.Password);

        if (!creatingResult.Succeeded)
            throw new FailedSignUpException(user, creatingResult);

        var roleAddingResult = await _userManager.AddToRoleAsync(user, role.ToString());

        if (!roleAddingResult.Succeeded)
            throw new FailedSignUpException(user, roleAddingResult);

        await _claimProvider.ProvideClaimsAsync(user, role);

        return user;
    }
    private ViewResult UserNotFound(LoginViewModel viewModel)
    {
        ModelState.AddModelError("",
            "Unknown username or password");
        return View("SignIn", viewModel);
    }
}