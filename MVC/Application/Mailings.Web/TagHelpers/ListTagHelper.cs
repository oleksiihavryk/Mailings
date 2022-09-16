using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mailings.Web.TagHelpers;
[HtmlTargetElement(ElementTag, Attributes = "struct")]
public sealed class ListTagHelper : TagHelper
{
    public const string ElementTag = "list";
    public const string ParentKey = "child element class";
    public const string HeaderKey = "header element class";

    public ListStructType Struct { get; set; }

    public override async Task ProcessAsync(
        TagHelperContext context, 
        TagHelperOutput output)
    {
        await base.ProcessAsync(context, output);

        var classes = DefineClasses();

        context.Items[ParentKey] = classes.Sibling;
        context.Items[HeaderKey] = classes.Header;

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
                Header = "block-list-header"
            },
            ListStructType.Text => new
            {
                Parent = "text-list",
                Sibling = "text-list-element",
                Header = "text-list-header"
            },
            ListStructType.Entity => new
            {
                Parent = "entity-list",
                Sibling = "entity-list-element",
                Header = "entity-list-header"
            },
            _ => throw new InvalidOperationException(
                "Struct field is not initialized")
        };
}