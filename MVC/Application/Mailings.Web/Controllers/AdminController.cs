using Mailings.Web.Core.Services.Interfaces;
using Mailings.Web.Shared.SystemConstants;
using Mailings.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.Controllers;
/// <summary>
///     Controller for support site administration
/// </summary>
[Authorize(AuthorizationPolicyConstants.Admin)]
public sealed class AdminController : Controller
{
    /// <summary>
    ///     View of admin functions
    /// </summary>
    /// <returns>
    ///     View of admin panel
    /// </returns>
    public ViewResult Panel() => View();
    /// <summary>
    ///     Function of generation account
    /// </summary>
    /// <returns>
    ///     View of account generation panel
    /// </returns>
    public ViewResult GenerateAccount() => View();
    /// <summary>
    ///     Invoked when account is generated
    /// </summary>
    /// <param name="viewModel">
    ///     View model of generated account
    /// </param>
    /// <returns>
    ///     View of generated account
    /// </returns>
    public ViewResult AccountIsGenerated(
        [FromQuery]GeneratedAccountViewModel viewModel) 
        => View(viewModel);
    /// <summary>
    ///     Account generation method
    /// </summary>
    /// <param name="betaTestService">
    ///     Service to which request with account generation is sending
    /// </param>
    /// <returns>
    ///     Redirect to generated account action
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Occurred when response from service is incorrect
    /// </exception>
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