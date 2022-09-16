using Mailings.Web.Controllers;
using Mailings.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.Components;
public class PaginationViewComponent : ViewComponent
{
    public const int MaxIncludedElements = 5;

    public IViewComponentResult Invoke(
        PaginationViewModel viewModel,
        string action,
        string controller)
    {
        var numList = new List<int>();

        var sideElementsCount = MaxIncludedElements / 2;
        for (int i = 0; i < MaxIncludedElements; i++)
        {
            int index = viewModel.PageIndex - sideElementsCount + i;
            if (index >= MailsController.StartPageIndex && 
                index <= viewModel.TotalPages)
            {
                numList.Add(index);
            }
        }

        ViewBag.FirstPage = MailsController.StartPageIndex;
        ViewBag.LastPage = viewModel.TotalPages;

        ViewBag.Action = action;
        ViewBag.Controller = controller;

        //numList is pagination indexes (pages which show to user in page on pagination)
        return View(numList);
    }
}