using IdentityServer4;
using Mailings.Authentication.API.Dto;
using Mailings.Authentication.Shared;
using Mailings.Authentication.Shared.ClaimProvider;
using Mailings.Authentication.Shared.PasswordGenerator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Authentication.API.Controllers;
[ApiController]
[Route("/api/account/beta-test")]
[Authorize(Policy = IdentityServerConstants.LocalApi.PolicyName)]
public class BetaTestController : ControllerBase
{
    private readonly IPasswordGenerator _passwordGen;
    private readonly UserManager<User> _userManager;
    private readonly IClaimProvider<User> _claimProvider;

    public BetaTestController(
        IPasswordGenerator passwordGen, 
        UserManager<User> userManager,
        IClaimProvider<User> claimProvider)
    {
        _passwordGen = passwordGen;
        _userManager = userManager;
        _claimProvider = claimProvider;
    }

    [HttpPost]
    public async Task<IActionResult> GenerateAccount()
    {
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
        await _claimProvider.ProvideClaimsAsync(user, Roles.BetaTester);

        if (result.Succeeded)
            return Ok(new ResponseDto()
            {
                IsSuccess = true,
                Messages = Array.Empty<string>(),
                Result = new GeneratedUserDto()
                {
                    Email = user.Email,
                    Password = pass,
                    UserName = user.UserName
                },
                StatusCode = StatusCodes.Status201Created
            });

        return Ok(new ResponseDto()
        {
            IsSuccess = false,
            Messages = new[]
                {"Unknown error while saving beta test account in system."},
            Result = null,
            StatusCode = StatusCodes.Status500InternalServerError
        });
    }
}