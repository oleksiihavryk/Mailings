using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.API.Controllers;
public class HomeController : Controller
{
    public ViewResult Index() => View();

    [Authorize]
    public IActionResult Login() => RedirectToAction(nameof(Index));
    [Authorize]
    public IActionResult Logout() => SignOut("oidc", "Cookies");
}