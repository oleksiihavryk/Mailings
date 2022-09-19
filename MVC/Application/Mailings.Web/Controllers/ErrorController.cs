using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.Controllers;
/// <summary>
///     Error handler controller
/// </summary>
public sealed class ErrorController : Controller
{
    /// <summary>
    ///     Error handler action
    /// </summary>
    /// <returns>
    ///     View of error
    /// </returns>
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

        ViewBag.EmailLink = emailLink;

        return View((object)path);
    }
}