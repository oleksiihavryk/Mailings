using Mailings.Web.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.API.Components;
public sealed class MailingEntityViewComponent : ViewComponent
{
    private const int MaxSymbolsCountInToString = 75;

    public IViewComponentResult Invoke(MailingViewModel viewModel)
    {
        var toStr = string.Join(", ", viewModel.To);

        @ViewBag.FormattedSenders = toStr.Length > MaxSymbolsCountInToString ?
            toStr.Substring(0, MaxSymbolsCountInToString) + "..." :
            toStr;

        return View(viewModel);
    } 
}
