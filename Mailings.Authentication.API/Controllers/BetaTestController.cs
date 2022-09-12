using IdentityServer4;
using Mailings.Authentication.API.Dto;
using Mailings.Authentication.API.ResponseFactory;
using Mailings.Authentication.Shared;
using Mailings.Authentication.Shared.ClaimProvider;
using Mailings.Authentication.Shared.PasswordGenerator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Authentication.API.Controllers;
/// <summary>
///     Api controller for provide creating a new beta test accounts.
/// </summary>
[ApiController]
[Route("/api/account/beta-test")]
[Authorize(Policy = IdentityServerConstants.LocalApi.PolicyName)]
public sealed class BetaTestController : ControllerBase
{
    private readonly IPasswordGenerator _passwordGen;
    private readonly UserManager<User> _userManager;
    private readonly IClaimProvider<User> _claimProvider;
    private readonly IResponseFactory _response;

    public BetaTestController(
        IPasswordGenerator passwordGen, 
        IClaimProvider<User> claimProvider, 
        IResponseFactory response,
        UserManager<User> userManager)
    {
        _passwordGen = passwordGen;
        _userManager = userManager;
        _claimProvider = claimProvider;
        _response = response;
    }

    /// <summary>
    ///     Endpoint of generating a new beta test accounts.
    /// </summary>
    /// <returns>Result object of generated beta test account.</returns>
    [HttpPost]
    public async Task<IActionResult> GenerateAccount()
    {
        Response? response = null;
        var id = Guid.NewGuid();
        var user = new User()
        {
            Email = "unexistedemail." + id.ToString() + "@gmail.com",
            EmailConfirmed = true,
            UserName = "beta-test." + id,
            FirstName = "Generated",
            LastName = "Beta Test"
        };
        string pass = _passwordGen.Generate();

        var result = await _userManager.CreateAsync(user, pass);
        await _userManager.AddToRoleAsync(user, Roles.BetaTester.ToString());
        await _claimProvider.ProvideClaimsAsync(user, Roles.BetaTester);

        var userData = new UserDataDto()
        {
            Email = user.Email,
            Password = pass,
            Username = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
        };

        response = result.Succeeded ? _response.CreateSuccess(
                successType: SuccessResponseType.Created,
                result: userData) : _response.EmptyInternalServerError;

        return Ok(response);
    }
}