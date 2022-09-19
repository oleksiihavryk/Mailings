using Mailings.Web.Core.Services.Interfaces;
using Mailings.Web.Shared.SystemConstants;
using Mailings.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.Controllers;
[Authorize(AuthorizationPolicyConstants.Admin)]
public sealed class AdminController : Controller
{
    public ViewResult Panel() => View();
    public ViewResult GenerateAccount() => View();
    public ViewResult AccountIsGenerated(
        [FromQuery]GeneratedAccountViewModel viewModel) 
        => View(viewModel);
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<RedirectToActionResult> TryToGenerateAccount(
        [FromServices]IBetaTestAuthenticationService betaTestService)
    {
        var acc = await betaTestService.GenerateAccount();

        var viewModel = new GeneratedAccountViewModel()
        {
            Email = acc.Email ?? throw new InvalidOperationException(
                message: "Service is not respond with correct data inside. " +
                "Impossible error in common situation"),
            Password = acc.Password,
            UserName = acc.Username
        };

        return RedirectToAction(nameof(AccountIsGenerated), viewModel);
    }
}