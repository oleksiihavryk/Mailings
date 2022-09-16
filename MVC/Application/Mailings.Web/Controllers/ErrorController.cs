using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.Controllers;
public sealed class ErrorController : Controller
{
    public ViewResult Index()
    {
        var eh = HttpContext.RequestServices
            .GetRequiredService<IExceptionHandlerFeature>();

        string path = eh.Path;

        string subject = "Mailings bug";
        string to = "oleksii.havryk2004@gmail.com";
        string message = "Hey, i found a bug on your site. Please fix this. " +
                         $"This is a path when bug is occurred: {path}";
        string emailLink = $"https://mail.google.com/mail/?" +
                           $"view=cm&" +
                           $"fs=1&" +
                           $"to={to}&" +
                           $"su={subject}&" +
                           $"body={message}&";

        ViewBag.EmailLing = emailLink;

        return View((object)path);
    }
}