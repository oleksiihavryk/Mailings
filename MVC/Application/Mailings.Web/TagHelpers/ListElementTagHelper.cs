using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mailings.Web.TagHelpers;
/// <summary>
///     Tag helper for create list elements
/// </summary>
[HtmlTargetElement(ElementTag, ParentTag = ListTagHelper.ElementTag)]
public sealed class ListElementTagHelper : TagHelper
{
    /// <summary>
    ///     Tag element 
    /// </summary>
    public const string ElementTag = "el";

    /// <summary>
    ///     Processing and generation a list element on page
    /// </summary>
    /// <param name="context">
    ///     Context of tag helpers
    /// </param>
    /// <param name="output">
    ///     Output of tag helpers
    /// </param>
    /// <returns>
    ///     Task of async operation by generation a list element
    /// </returns>
    public override async Task ProcessAsync(
        TagHelperContext context,
        TagHelperOutput output)
    {
        await base.ProcessAsync(context, output);

        var elemClass = (string)context.Items[ListTagHelper.ParentKey];

        output.TagName = "li";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.AddClass(elemClass, HtmlEncoder.Default);
    }
}