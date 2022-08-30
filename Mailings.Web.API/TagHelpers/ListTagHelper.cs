using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mailings.Web.API.TagHelpers;
[HtmlTargetElement(ElementTag, Attributes = "struct")]
public sealed class ListTagHelper : TagHelper
{
    public const string ElementTag = "list";
    public const string ParentKey = "child element class";

    public ListStructType Struct { get; set; }

    public override async Task ProcessAsync(
        TagHelperContext context, 
        TagHelperOutput output)
    {
        await base.ProcessAsync(context, output);

        var classes = DefineClasses();

        context.Items[ParentKey] = classes.Sibling;

        output.TagMode = TagMode.StartTagAndEndTag;
        output.TagName = "ul";
        output.Attributes.Add("class", classes.Parent);
    }

    private dynamic DefineClasses()
        => Struct switch
        {
            ListStructType.Block => new
            {
                Parent = "block-list",
                Sibling = "block-list-element",
            },
            ListStructType.Text => new
            {
                Parent = "text-list",
                Sibling = "text-list-element"
            },
            ListStructType.Entity => new
            {
                Parent = "entity-list",
                Sibling = "entity-list-element"
            },
            _ => throw new InvalidOperationException(
                "Struct field is not initialized")
        };
}