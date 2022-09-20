using System.ComponentModel.DataAnnotations;

namespace Mailings.Web.ViewModels;
/// <summary>
///     Mail view model
/// </summary>
public sealed class MailViewModel
{
    /// <summary>
    ///     Mail identifier
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    ///     Mail theme
    /// </summary>
    [Required] public string Theme { get; set; } = string.Empty;
    /// <summary>
    ///     Mail content
    /// </summary>
    [Required, DataType(DataType.MultilineText)] 
    public string Content { get; set; } = string.Empty;
    /// <summary>
    ///     Mail type
    /// </summary>
    [Required] public MailTypeViewModel Type { get; set; } = MailTypeViewModel.Unknown;
    /// <summary>
    ///     Mail attachments
    /// </summary>
    public List<IFormFile> Attachments { get; set; } = new List<IFormFile>();
}