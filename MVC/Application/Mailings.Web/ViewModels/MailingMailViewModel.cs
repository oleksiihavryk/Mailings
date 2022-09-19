using System.ComponentModel.DataAnnotations;

namespace Mailings.Web.ViewModels;

public sealed class MailingMailViewModel
{
    [Required] public string Id { get; set; } = string.Empty;
    [Required] public string Theme { get; set; } = string.Empty;
    [Required] public string Type { get; set; } = string.Empty;
}