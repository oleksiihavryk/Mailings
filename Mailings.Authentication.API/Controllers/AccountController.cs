using IdentityServer4.Services;
using Mailings.Authentication.API.ViewModels;
using Mailings.Authentication.Shared;
using Mailings.Authentication.Shared.ClaimProvider;
using Mailings.Authentication.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet]
    [Route("[action]")]
    public ViewResult Login(string returnUrl) => View((object)returnUrl);
    [HttpGet]
    [Route("[action]")]
    public ViewResult SignIn(string returnUrl)
    {
        var viewModel = new LoginViewModel()
        {
            ReturnUrl = returnUrl
        };
        return View(viewModel);
    }
    [HttpGet]
    [Route("[action]")]
    public ViewResult Register()
    {
        string returnUrl = ViewBag.ReturnUrl;
        var viewModel = new RegisterViewModel()
        {
            ReturnUrl = returnUrl
        };
        return View(viewModel);
    }
    [HttpGet]
    [Route("[action]")]
    public async Task<RedirectResult> Logout(string logoutId)
    {
        await _signInManager.SignOutAsync();
        var context =await _interactionService.GetLogoutContextAsync(logoutId);
        return Redirect(context.PostLogoutRedirectUri);
    }
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> SignIn(LoginViewModel viewModel)
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

        if (!ModelState.IsValid)
                return View(viewModel);

        if (viewModel.Password != viewModel.PasswordConfirmation)
        {
            ModelState.AddModelError(
                key: "",
                errorMessage: "Password and confirmation of password is not equals");
            return View();
        }

        User? user = null;
        try 
        {
            user = await CreateUserAsync(viewModel);
            await _claimProvider.ProvideClaimsAsync(user, Roles.Default);
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
            await _userManager.DeleteAsync(user);
            throw;
        }
        catch (FailedSignUpException)
        {
            await _userManager.DeleteAsync(user);
            throw;
        }
    }

    private async Task<User> CreateUserAsync(RegisterViewModel viewModel)
    {
        User user = new User()
        {
            UserName = viewModel.Username,
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            Email = viewModel.Email,
        };

        var creatingResult = await _userManager.CreateAsync(user, viewModel.Password);

        if (!creatingResult.Succeeded)
        {
            throw new FailedSignUpException(user, creatingResult);
        }

        var roleAddingResult = await _userManager.AddToRoleAsync(user, Roles.Default.ToString());

        if (!roleAddingResult.Succeeded)
        {
            throw new FailedSignUpException(user, roleAddingResult);
        }

        return user;
    }
    private ViewResult UserNotFound(LoginViewModel viewModel)
    {
        ModelState.AddModelError("",
            "Unknown username or password");
        return View("SignIn", viewModel);
    }
}