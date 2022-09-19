using Mailings.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.Components;
/// <summary>
///     Mailing entity view component
/// </summary>
public sealed class MailingEntityViewComponent : ViewComponent
{
    /// <summary>
    ///     Maximal count of symbols of formatted senders string
    /// </summary>
    private const int MaxSymbolsCountInToString = 75;

    /// <summary>
    ///     Create view of mail entity
    /// </summary>
    /// <param name="viewModel">
    ///     View component model data
    /// </param>
    /// <returns>
    ///     Component view
    /// </returns>
    public IViewComponentResult Invoke(MailingViewModel viewModel)
    {
        var toStr = string.Join(", ", viewModel.To);

        @ViewBag.FormattedSenders = toStr.Length > MaxSymbolsCountInToString ?
            toStr.Substring(0, MaxSymbolsCountInToString) + "..." :
            toStr;

        return View(viewModel);
    } 
}
