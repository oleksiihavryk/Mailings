using System.ComponentModel.DataAnnotations;

namespace Mailings.Web.ViewModels;
/// <summary>
///     Mailing view model
/// </summary>
public sealed class MailingViewModel
{
    /// <summary>
    ///     Mailing identifier
    /// </summary>
    [Required] public Guid Id { get; set; } = Guid.Empty;
    /// <summary>
    ///     Mail view model
    /// </summary>
    [Required] public MailingMailViewModel Mail { get; set; } = new MailingMailViewModel();
    /// <summary>
    ///     Mailing name
    /// </summary>
    [Required] public string Name { get; set; } = string.Empty;
    /// <summary>
    ///     All mail receivers
    /// </summary>
    [Required] public List<string> To { get; set; } = new List<string>();
}