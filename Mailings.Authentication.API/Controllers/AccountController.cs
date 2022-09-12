using IdentityServer4.Services;
using Mailings.Authentication.API.ViewModels;
using Mailings.Authentication.Shared;
using Mailings.Authentication.Shared.ClaimProvider;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mailings.Authentication.API.Exceptions;

namespace Mailings.Authentication.API.Controllers;
/// <summary>
///     Controller responsible for creating account, login and logout to/from account.
/// </summary>
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

    /// <summary>
    ///     Endpoint of choosing login type to account
    /// </summary>
    /// <param name="returnUrl">
    ///     Link to which user was been returned after login in system
    /// </param>
    /// <returns>Login View</returns>
    [HttpGet("[action]")]
    public async Task<ViewResult> Login([FromQuery]string returnUrl) => 
        await Task.Run(() => View((object)returnUrl));
    /// <summary>
    ///     Endpoint of login to account
    /// </summary>
    /// <param name="returnUrl">
    ///     Link to which user was been returned after login in system
    /// </param>
    /// <returns>SignIn View</returns>
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
    /// <summary>
    ///     Endpoint of creating a new account
    /// </summary>
    /// <param name="returnUrl">
    ///     Link to which user was been returned after login in system
    /// </param>
    /// <returns>Register View</returns>
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
    /// <summary>
    ///     Endpoint of logout from account
    /// </summary>
    /// <param name="logoutId">
    ///     Id of request to logout from system.
    /// </param>
    /// <returns>Redirect to page from which logout has been executed</returns>
    [HttpGet("[action]")]
    public async Task<RedirectResult> Logout([FromQuery] string logoutId)
    {
        await _signInManager.SignOutAsync();
        var context =await _interactionService.GetLogoutContextAsync(logoutId);
        return Redirect(context.PostLogoutRedirectUri);
    }
    /// <summary>
    ///     Endpoint of login to account (By submit form)
    /// </summary>
    /// <param name="viewModel">Account data from form</param>
    /// <returns>Result of login in system.</returns>
    [HttpPost("[action]")]
    [ValidateAntiForgeryToken]
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
    /// <summary>
    ///     Endpoint of registering a new account (By submit form)
    /// </summary>
    /// <param name="viewModel">Account data from form</param>
    /// <returns>Result of register new account in system.</returns>
    [HttpPost("[action]")]
    [ValidateAntiForgeryToken]
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

    /// <summary>
    ///     Creating a new user to system (with providing claims and default role)
    /// </summary>
    /// <param name="viewModel">
    ///     Model of registration account data
    /// </param>
    /// <returns>
    ///     Created user in system.
    /// </returns>
    /// <exception cref="FailedSignUpException">
    ///     Is error of failed sign up process.
    /// </exception>
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
    /// <summary>
    ///     Invoked if user is not found in system.
    /// </summary>
    /// <param name="viewModel">
    ///     Login user data.
    /// </param>
    /// <returns>
    ///     Sign in view with error of "unknown username of password".
    /// </returns>
    private ViewResult UserNotFound(LoginViewModel viewModel)
    {
        ModelState.AddModelError("",
            "Unknown username or password");
        return View("SignIn", viewModel);
    }
}