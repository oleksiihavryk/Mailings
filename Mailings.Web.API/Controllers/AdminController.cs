using Mailings.Web.API.ViewModels;
using Mailings.Web.Services;
using Mailings.Web.Shared.Dto;
using Mailings.Web.Shared.SystemConstants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
 
namespace Mailings.Web.API.Controllers;
[Authorize(AuthorizationPolicyConstants.Admin)]
public sealed class AdminController : Controller
{
    private readonly IBetaTestAuthenticationService _betaTestService;

    public AdminController(IBetaTestAuthenticationService betaTestService)
    {
        _betaTestService = betaTestService;
    }

    public ViewResult Panel() => View();
    public ViewResult GenerateAccount() => View();
    public ViewResult AccountIsGenerated(GeneratedAccountViewModel viewModel)
        => View(viewModel);
    [HttpPost]
    public async Task<RedirectToActionResult> TryToGenerateAccount()
    {
        var acc = await _betaTestService.GenerateAccount();

        var viewModel = new GeneratedAccountViewModel()
        {
            Email = acc.Email,
            Password = acc.Password,
            UserName = acc.Username
        };

        return RedirectToAction(nameof(AccountIsGenerated), viewModel);
    }
}