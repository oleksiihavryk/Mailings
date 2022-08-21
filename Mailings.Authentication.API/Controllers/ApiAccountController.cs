using IdentityServer4;
using Mailings.Authentication.API.Dto;
using Mailings.Authentication.Shared;
using Mailings.Authentication.Shared.ClaimProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Authentication.API.Controllers;
[ApiController]
[Route("api/account")]
[Authorize(Policy = IdentityServerConstants.LocalApi.PolicyName)]
public sealed class ApiAccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IClaimProvider<User> _claimProvider;

    public ApiAccountController(
        UserManager<User> userManager,
        IClaimProvider<User> claimProvider)
    {
        _userManager = userManager;
        _claimProvider = claimProvider;
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> Change([FromBody][FromForm]UserDataDto userData)
    {
        var user = await _userManager.FindByNameAsync(userData.Username);

        if (user != null)
        {
            if (userData.FirstName != null)
                user.FirstName = userData.FirstName;
            if (userData.LastName != null)
                user.LastName = userData.LastName;
            if (userData.Email != null)
            {
                user.Email = userData.Email;
                user.EmailConfirmed = false;
            }

            await RecreateClaimsAsync(user);

            return Ok(new ResponseDto()
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status205ResetContent
            });
        }

        return Ok(new ResponseDto()
        {
            IsSuccess = false,
            Messages = new[] { "User not found" },
            Result = null,
            StatusCode = StatusCodes.Status404NotFound
        });
    }

    private async Task RecreateClaimsAsync(User user)
    {
        var role = (await _userManager.GetRolesAsync(user)).First();
        await _claimProvider.RevertClaimsAsync(user);
        await _claimProvider.ProvideClaimsAsync(user, Enum.Parse<Roles>(role));
    }
}