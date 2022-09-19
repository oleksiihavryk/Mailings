using System.Security.Claims;
using IdentityModel;
using Mailings.Web.Core.Services.Interfaces;
using Mailings.Web.Domain.ServicesModels;
using Mailings.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.Controllers;
/// <summary>
///     User profile controller
/// </summary>
[Authorize]
public sealed class UserController : Controller
{
    /// <summary>
    ///     Account controller service
    /// </summary>
    private readonly IAccountAuthenticationService _accountService;

    public UserController(IAccountAuthenticationService accountService)
    {
        _accountService = accountService;
    }

    /// <summary>
    ///     User profile view
    /// </summary>
    /// <returns>
    ///     View of user profile
    /// </returns>
    public ViewResult Profile() => View(model: PrepareUserData());
    /// <summary>
    ///     View for changing profile data
    /// </summary>
    /// <returns>
    ///     View with form for changing profile data
    /// </returns>
    public ViewResult Change()
    {
        var userData = PrepareUserData();

        return View(new ChangingUserDataViewModel()
        {
            Email = userData.Email,
            FirstName = userData.FirstName,
            LastName = userData.LastName,
        });
    }
    /// <summary>
    ///     Changing profile data by form input
    /// </summary>
    /// <param name="viewModel">
    ///     Form input view model
    /// </param>
    /// <returns>
    ///     Logout from system and changing profile data
    /// </returns>
    [ValidateAntiForgeryToken, HttpPost]
    public async Task<IActionResult> Change([FromForm]ChangingUserDataViewModel viewModel)
    {
        var userData = PrepareChangingUserData(viewModel);

        await _accountService.ChangeUserData(userData);

        return RedirectToAction("Logout", "Home");
    }

    /// <summary>
    ///     Prepare to changing user data 
    /// </summary>
    /// <param name="viewModel">
    ///     View model from form to changing user data
    /// </param>
    /// <returns>
    ///     Model service user data
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Occurred when user is not logged in system
    /// </exception>
    private UserData PrepareChangingUserData(ChangingUserDataViewModel viewModel)
    {
        var userClaims = (User.Identity as ClaimsIdentity)?.Claims;

        if (userClaims == null)
            throw new InvalidOperationException(
                "User is must be authorized on the system!");

        var email = userClaims
            .FirstOrDefault(c => c.Type == ClaimTypes.Email)?
            .Value;
        var firstName = userClaims
            .FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?
            .Value;
        var lastName = userClaims
            .FirstOrDefault(c => c.Type == ClaimTypes.Surname)?
            .Value;
        var username = userClaims
            .FirstOrDefault(c => c.Type == JwtClaimTypes.PreferredUserName)?
            .Value ?? throw new InvalidOperationException("Impossible error");

        var userDataDto = new UserData()
        {
            Email = email != viewModel.Email ? 
                viewModel.Email : null,
            FirstName = firstName != viewModel.FirstName ? 
                viewModel.FirstName : null,
            LastName = lastName != viewModel.LastName ? 
                viewModel.LastName : null,
            Username = username
        };

        return userDataDto;
    }
    /// <summary>
    ///     Prepare user data from authenticate user claims
    /// </summary>
    /// <returns>
    ///     View model of user data
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Occurred when user is not authentication in system
    /// </exception>
    private UserDataViewModel PrepareUserData()
    {
        var userClaims = (User.Identity as ClaimsIdentity)?.Claims;

        if (userClaims == null)
            throw new InvalidOperationException(
                "User is must be authorized on the system!");

        var arrayUserClaims = userClaims as Claim[] ?? userClaims.ToArray();
        var userData = new UserDataViewModel()
        {
            Email = arrayUserClaims
                .FirstOrDefault(c => c.Type == ClaimTypes.Email)?
                .Value ?? "[email address is missing]",
            FirstName = arrayUserClaims
                .FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?
                .Value ?? "[first name is missing]",
            LastName = arrayUserClaims
                .FirstOrDefault(c => c.Type == ClaimTypes.Surname)?
                .Value ?? "[last name is missing]",
            UserName = arrayUserClaims
                .FirstOrDefault(c => c.Type == JwtClaimTypes.PreferredUserName)?
                .Value ?? "[username is missing]",
            Role = arrayUserClaims
                .FirstOrDefault(c => c.Type == ClaimTypes.Role)?
                .Value ?? "[role is missing]"
        };

        return userData;
    }
}