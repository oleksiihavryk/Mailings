using Mailings.Web.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.API.Components;
public sealed class MailingEntityViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(MailingViewModel viewModel)
        => View(viewModel);
}
