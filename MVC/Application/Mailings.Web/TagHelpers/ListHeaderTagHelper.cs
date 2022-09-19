using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mailings.Web.TagHelpers;
/// <summary>
///     Tag helper of list header
/// </summary>
[HtmlTargetElement(TagElement)]
public class ListHeaderTagHelper : TagHelper
{
    /// <summary>
    ///     Element tag
    /// </summary>
    public const string TagElement = "elhead";

    /// <summary>
    ///     Processing and generation of list header tag helper
    /// </summary>
    /// <param name="context">
    ///     Context of tag helpers
    /// </param>
    /// <param name="output">
    ///     Output of tag helpers
    /// </param>
    /// <returns>
    ///     Task of async operation by generation of list header
    /// </returns>
    public override async Task ProcessAsync(
        TagHelperContext context,
        TagHelperOutput output)
    {
        await base.ProcessAsync(context, output);

        var elementClass = context.Items[ListTagHelper.HeaderKey] as string;

        output.TagMode = TagMode.StartTagAndEndTag;
        output.TagName = "li";
        output.AddClass(elementClass, HtmlEncoder.Default);
    }
}