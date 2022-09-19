using Mailings.Web.Shared.SystemConstants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.Controllers;
/// <summary>
///     Main page and default site functions controller
/// </summary>
public sealed class HomeController : Controller
{
    /// <summary>
    ///     Main page action
    /// </summary>
    /// <returns>
    ///     View of main page
    /// </returns>
    public ViewResult Index()
    {
        string subject = "Mailings beta test access";
        string to = "oleksii.havryk2004@gmail.com";
        string message = "Write here your message";
        string writeToEmailLink = $"https://mail.google.com/mail/?" +
                                  $"view=cm&" +
                                  $"fs=1&" +
                                  $"to={to}&" +
                                  $"su={subject}&" +
                                  $"body={message}&";

        ViewBag.WriteToEmailLink = writeToEmailLink;

        return View();
    }
    /// <summary>
    ///     Login to site
    /// </summary>
    /// <returns>
    ///     Redirect to main page
    /// </returns>
    [Authorize]
    public RedirectToActionResult Login() => RedirectToAction(nameof(Index));
    /// <summary>
    ///     Logout from site
    /// </summary>
    /// <returns>
    ///     Redirect to main page
    /// </returns>
    [Authorize]
    public SignOutResult Logout() => SignOut(
        AuthenticationSchemes.OidcScheme, 
        AuthenticationSchemes.CookiesScheme);
}