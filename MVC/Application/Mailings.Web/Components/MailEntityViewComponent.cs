using Mailings.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Mailings.Web.Components;
/// <summary>
///     Mail entity view component
/// </summary>
public class MailEntityViewComponent : ViewComponent
{
    /// <summary>
    ///     Create view of mail entity
    /// </summary>
    /// <param name="viewModel">
    ///     View component model data
    /// </param>
    /// <returns>
    ///     Component view
    /// </returns>
    public ViewViewComponentResult Invoke(MailViewModel viewModel)
    {
        ViewBag.FormattedContent = viewModel.Content.Length > 60 ? 
            viewModel.Content.Substring(0, 60) + "..." : 
            viewModel.Content;

        return View(viewModel);
    } 
}