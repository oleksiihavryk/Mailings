using System.Security.Claims;
using IdentityModel;
using Mailings.Web.API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.API.Controllers;

[Authorize]
public class UserController : Controller
{
    public IActionResult Profile()
    {
        var userClaims = (User.Identity as ClaimsIdentity)?.Claims;

        if (userClaims == null)
            throw new InvalidOperationException(
                "User is must be authorized on the system!");

        var userData = PrepareUserData(userClaims);

        return View(userData);
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