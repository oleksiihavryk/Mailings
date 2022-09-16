using IdentityServer4;
using Mailings.Authentication.Core.ResponseFactory;
using Mailings.Authentication.Data.ClaimProvider;
using Mailings.Authentication.Domain;
using Mailings.Authentication.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Authentication.Controllers;
/// <summary>
///     Api controller for provide access to changing account data.
/// </summary>
[ApiController]
[Route("api/account")]
[Authorize(Policy = IdentityServerConstants.LocalApi.PolicyName)]
public sealed class ApiAccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IClaimProvider<User> _claimProvider;
    private readonly IResponseFactory _response;

    public ApiAccountController(
        UserManager<User> userManager,
        IClaimProvider<User> claimProvider, 
        IResponseFactory response)
    {
        _userManager = userManager;
        _claimProvider = claimProvider;
        _response = response;
    }

    /// <summary>
    ///     Endpoint of changing private account data
    /// </summary>
    /// <param name="userData">User data which exactly was been changed.</param>
    /// <returns>Result object of current api result.</returns>
    [HttpPut]
    public async Task<IActionResult> Change(UserDataDto userData)
    {
        Response? response = null;
        var user = await _userManager.FindByNameAsync(userData.Username);

        if (user != null)
        {
            if (userData.Email == null || 
                await _userManager.FindByEmailAsync(userData.Email) != null)
            {
                if (!string.IsNullOrWhiteSpace(userData.FirstName) ||
                    !string.IsNullOrWhiteSpace(userData.LastName) ||
                    !string.IsNullOrWhiteSpace(userData.Email))
                {
                    Enum.TryParse<Roles>(
                        value: (await _userManager
                            .GetRolesAsync(user))
                        .First(), 
                        result: out var role);
                    await _claimProvider.RevertClaimsAsync(user);

                    if (!string.IsNullOrWhiteSpace(userData.FirstName))
                        user.FirstName = userData.FirstName;
                    if (!string.IsNullOrWhiteSpace(userData.LastName))
                        user.LastName = userData.LastName;
                    if (!string.IsNullOrWhiteSpace(userData.Email))
                    {
                        user.Email = userData.Email;
                        user.EmailConfirmed = false;
                    }

                    await _claimProvider.ProvideClaimsAsync(user, role);
                }
                response = _response.CreateSuccess(SuccessResponseType.Changed);
            }
            else
            {
                response = _response.CreateFailedResponse(
                    failedType: FailedResponseType.BadRequest,
                    message: "User with current email is already exist in system");
            }
        }
        else
        {
            response = _response.CreateFailedResponse(message: "User not found");
        }

        return Ok(response);
    }
}