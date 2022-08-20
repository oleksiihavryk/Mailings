using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.API.Controllers;

public class ErrorController : Controller
{
    public IActionResult Index()
    {
        var eh = HttpContext.RequestServices
            .GetRequiredService<IExceptionHandlerFeature>();

        string path = eh.Path;

        return View((object)path);
    }
}