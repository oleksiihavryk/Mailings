using Mailings.Web.API.ViewModels;
using Mailings.Web.Services;
using Mailings.Web.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
 
namespace Mailings.Web.API.Controllers;
[Authorize("Admin")]
internal sealed class AdminController : Controller
{
    private readonly IBetaTestAuthenticationService _betaTestService;

    public AdminController(IBetaTestAuthenticationService betaTestService)
    {
        _betaTestService = betaTestService;
    }

    public IActionResult Panel() => View();
    public IActionResult GenerateAccount() => View();
    [HttpPost]
    public async Task<IActionResult> TryToGenerateAccount()
    {
        var acc = await _betaTestService.GenerateAccount();

        var viewModel = GetViewModel(acc);

        return RedirectToAction(nameof(AccountIsGenerated), viewModel);
    }
    public IActionResult AccountIsGenerated(GeneratedAccountViewModel viewModel) 
        => View(viewModel); 

    private GeneratedAccountViewModel GetViewModel(GeneratedUserDto acc)
        => new GeneratedAccountViewModel()
        {
            Email = acc.Email,
            Password = acc.Password,
            UserName = acc.UserName
        };
}