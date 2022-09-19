using System.ComponentModel.DataAnnotations.Schema;
using Mailings.Web.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mailings.Web.TagHelpers;

/// <summary>
///     Tag helper for buttons
/// </summary>
[HtmlTargetElement(ElementTag, Attributes = "action, controller")]
public sealed class BtnTagHelper : TagHelper
{
    /// <summary>
    ///     Element tag
    /// </summary>
    public const string ElementTag = "btn";
    /// <summary>
    ///     Class of tag
    /// </summary>
    public const string ElementClass = "button";

    private readonly IUrlHelperFactory _urlHelperFactory;

    /// <summary>
    ///     View context for creating a tag helper
    /// </summary>
    [NotMapped]
    [ViewContext]
    public ViewContext ViewContext { get; set; } = null!;

    /// <summary>
    ///     Action to which button will be redirect
    /// </summary>
    public string Action { get; set; } = null!;
    /// <summary>
    ///     Controller to which button will be redirect
    /// </summary>
    public string Controller { get; set; } = null!;
    /// <summary>
    ///     Route data
    /// </summary>
    public object? Data { get; set; } = null;

    public BtnTagHelper(IUrlHelperFactory urlHelperFactory)
    {
        _urlHelperFactory = urlHelperFactory;
    }

    /// <summary>
    ///     Process button generation
    /// </summary>
    /// <param name="context">
    ///     Tag helpers view context
    /// </param>
    /// <param name="output">
    ///     Tag helpers output
    /// </param>
    /// <returns>
    ///     Task of async operation by generation of button
    /// </returns>
    /// <exception cref="UrlIsNotGeneratedException"></exception>
    public override async Task ProcessAsync(
        TagHelperContext context,
        TagHelperOutput output)
    {
        await base.ProcessAsync(context, output);

        var urlHelper = _urlHelperFactory.GetUrlHelper(new ActionContext(
            actionDescriptor: ViewContext.ActionDescriptor,
            httpContext: ViewContext.HttpContext,
            routeData: ViewContext.RouteData,
            modelState: ViewContext.ModelState));

        var url = urlHelper.Action(Action, Controller, Data) ??
                  throw new UrlIsNotGeneratedException();

        output.TagName = "a";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.Add("class", ElementClass);
        output.Attributes.Add("href", url);
    }
}