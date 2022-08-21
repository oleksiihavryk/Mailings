using System.Security.Claims;
using IdentityModel;
using Mailings.Web.API.ViewModels;
using Mailings.Web.Services;
using Mailings.Web.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.API.Controllers;

[Authorize]
public sealed class UserController : Controller
{
    private readonly IAccountAuthenticationService _accountService;

    public UserController(IAccountAuthenticationService accountService)
    {
        _accountService = accountService;
    }

    public IActionResult Profile()
    {
        var userClaims = (User.Identity as ClaimsIdentity)?.Claims;

        if (userClaims == null)
            throw new InvalidOperationException(
                "User is must be authorized on the system!");

        var userData = PrepareUserData(userClaims);

        return View(userData);
    }
    public IActionResult Change()
    {
        var userClaims = (User.Identity as ClaimsIdentity)?.Claims;

        if (userClaims == null)
            throw new InvalidOperationException(
                "User is must be authorized on the system!");

        var userData = PrepareUserData(userClaims);

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

    private UserDataDto PrepareChangingUserData(ChangingUserDataViewModel viewModel)
    {
        var userClaims = (User.Identity as ClaimsIdentity)?.Claims;

        if (userClaims == null)
            throw new InvalidOperationException(
                "User is must be authorized on the system!");

        var userData = PrepareUserData(userClaims);

        var userDataDto = new UserDataDto()
        {
            Email = userData.Email != viewModel.Email ? 
                viewModel.Email : null,
            FirstName = userData.FirstName != viewModel.FirstName ? 
                viewModel.FirstName : null,
            LastName = userData.LastName != viewModel.LastName ? 
                viewModel.LastName : null,
            Username = userData.UserName
        };

        return userDataDto;
    }

    private UserDataViewModel PrepareUserData(IEnumerable<Claim> userClaims)
        => new UserDataViewModel()
        {
            Email = userClaims
                .FirstOrDefault(c => c.Type
                    .Contains(ClaimTypes.Email))?
                .Value ?? "[email address is missing]",
            FirstName = userClaims
                .FirstOrDefault(c => c.Type
                    .Contains(ClaimTypes.GivenName))?
                .Value ?? "[first name is missing]",
            LastName = userClaims
                .FirstOrDefault(c => c.Type
                    .Contains(ClaimTypes.Surname))?
                .Value ?? "[last name is missing]",
            UserName = userClaims
                .FirstOrDefault(c => c.Type
                    .Contains(JwtClaimTypes.PreferredUserName))?
                .Value ?? "[username is missing]",
            Role = userClaims
                .FirstOrDefault(c => c.Type
                    .Contains("/" + JwtClaimTypes.Role.Replace("_", "")))?
                .Value ?? "[role is missing]"
        };
}