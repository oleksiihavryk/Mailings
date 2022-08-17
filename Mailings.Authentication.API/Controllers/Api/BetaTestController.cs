using Mailings.Authentication.API.Dto;
using Mailings.Authentication.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Authentication.API.Controllers.Api;
[Route("api/beta-test")]
[ApiController]
[Authorize]
internal sealed class BetaTestController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public BetaTestController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("/account")]
    public async Task<IActionResult> GenerateAccount()
    {
        var user = new User()
        {
            Email = "em" + Guid.NewGuid().ToString() + "@gmail.com",
            EmailConfirmed = true,
            UserName = "beta-test." + Guid.NewGuid(),
            FirstName = "Generated",
            LastName = "Beta Test"
        };
        string pass = Guid.NewGuid().ToString();

        var result =  await _userManager.CreateAsync(user, pass);

        if (result.Succeeded)
            return Ok(new ResponseDto()
            {
                IsSuccess = true,
                Messages = Array.Empty<string>(),
                Result = new GeneratedUserDto()
                {

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