using Mailings.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.Components;

public class MailEntityViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(MailViewModel viewModel)
    {
        ViewBag.FormattedContent = viewModel.Content.Length > 60 ? 
            viewModel.Content.Substring(0, 60) + "..." : 
            viewModel.Content;

        return View(viewModel);
    } 
}