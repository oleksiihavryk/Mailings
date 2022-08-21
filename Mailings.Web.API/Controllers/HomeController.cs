using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.API.Controllers;

public sealed class HomeController : Controller
{
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
    [Authorize]
    public IActionResult Login() => RedirectToAction(nameof(Index));
    [Authorize]
    public IActionResult Logout() => SignOut("oidc", "Cookies");
}