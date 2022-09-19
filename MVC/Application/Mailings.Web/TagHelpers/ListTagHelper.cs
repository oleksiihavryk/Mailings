using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mailings.Web.TagHelpers;
/// <summary>
///     List tag helper
/// </summary>
[HtmlTargetElement(ElementTag, Attributes = "struct")]
public sealed class ListTagHelper : TagHelper
{
    /// <summary>
    ///     Element tag
    /// </summary>
    public const string ElementTag = "list";
    /// <summary>
    ///     Parent key for provide a class to his children elements
    /// </summary>
    public const string ParentKey = "child element class";
    /// <summary>
    ///     Header key for provider a class to his header
    /// </summary>
    public const string HeaderKey = "header element class";

    /// <summary>
    ///     List struct type
    /// </summary>
    public ListStructType Struct { get; set; }

    /// <summary>
    ///     Processing and generation a list tag helper with specified type
    /// </summary>
    /// <param name="context">
    ///     Context of tag helpers
    /// </param>
    /// <param name="output">
    ///     Output of tag helpers
    /// </param>
    /// <returns>
    ///     Task of async operation by generation a list
    /// </returns>
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

    /// <summary>
    ///     Define classes of tag helpers
    /// </summary>
    /// <returns>
    ///     Object with classes for every elements
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Occurred when list type is not unhandled with this method
    /// </exception>
    private ListClasses DefineClasses()
        => Struct switch
        {
            ListStructType.Block => new ListClasses(
                parent: "block-list",
                sibling: "block-list-element",
                header: "block-list-header"),
                ListStructType.Text => new ListClasses(
                parent: "text-list",
                sibling: "text-list-element",
                header: "text-list-header"),
            ListStructType.Entity => new ListClasses(
                parent: "entity-list",
                sibling: "entity-list-element",
                header: "entity-list-header"),
            _ => throw new InvalidOperationException(
                "Struct field is not initialized")
        };

    /// <summary>
    ///     Class what encapsulate css classes of every elements in list
    /// </summary>
    private class ListClasses
    {
        /// <summary>
        ///     Css class to list parent
        /// </summary>
        public string Parent { get; }
        /// <summary>
        ///     Css class of sibling element
        /// </summary>
        public string Sibling { get; }
        /// <summary>
        ///     Css class of header element
        /// </summary>
        public string Header { get; }

        public ListClasses(
            string parent, 
            string sibling, 
            string header)
        {
            Parent = parent;
            Sibling = sibling;
            Header = header;
        }
    }
}