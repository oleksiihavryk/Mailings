using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mailings.Web.TagHelpers;
[HtmlTargetElement("elhead")]
public class ListHeaderTagHelper : TagHelper
{
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