using System.ComponentModel.DataAnnotations;

namespace Mailings.Web.ViewModels;
/// <summary>
///     Mail of mailing group view model
/// </summary>
public sealed class MailingMailViewModel
{
    /// <summary>
    ///     Mail identifier
    /// </summary>
    [Required] public string Id { get; set; } = string.Empty;
    /// <summary>
    ///     Mail theme
    /// </summary>
    [Required] public string Theme { get; set; } = string.Empty;
    /// <summary>
    ///     Mail type
    /// </summary>
    [Required] public string Type { get; set; } = string.Empty;
}