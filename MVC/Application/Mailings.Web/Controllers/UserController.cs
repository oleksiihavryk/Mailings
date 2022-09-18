using System.Security.Claims;
using IdentityModel;
using Mailings.Web.Core.Services.Interfaces;
using Mailings.Web.Domain.ServicesModels;
using Mailings.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.Controllers;

[Authorize]
public sealed class UserController : Controller
{
    private readonly IAccountAuthenticationService _accountService;

    public UserController(IAccountAuthenticationService accountService)
    {
        _accountService = accountService;
    }

    public ViewResult Profile()
    {
        var userData = PrepareUserData();

        return View(userData);
    }
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
    [HttpPost]
    public async Task<IActionResult> Change(ChangingUserDataViewModel viewModel)
    {
        var userData = PrepareChangingUserData(viewModel);

        await _accountService.ChangeUserData(userData);

        return RedirectToAction("Logout", "Home");
    }

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