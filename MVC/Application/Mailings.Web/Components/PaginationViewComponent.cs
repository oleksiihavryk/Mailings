using Mailings.Web.Controllers;
using Mailings.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.Components;
/// <summary>
///     Pagination view component
/// </summary>
public class PaginationViewComponent : ViewComponent
{
    /// <summary>
    ///     Max included page links in pagination
    /// </summary>
    public const int MaxIncludedElements = 5;

    /// <summary>
    ///     Create a view of pagination on page
    /// </summary>
    /// <param name="viewModel">
    ///     Component model data of pagination
    /// </param>
    /// <param name="action">
    ///     Action which page anchor linked to
    /// </param>
    /// <param name="controller">
    ///     Controller which page anchor linked to
    /// </param>
    /// <returns>
    ///     View of component
    /// </returns>
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