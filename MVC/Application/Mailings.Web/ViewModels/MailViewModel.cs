using System.ComponentModel.DataAnnotations;

namespace Mailings.Web.ViewModels;

public sealed class MailViewModel
{
    public Guid Id { get; set; }
    [Required] public string Theme { get; set; } = string.Empty;
    [Required, DataType(DataType.MultilineText)] 
    public string Content { get; set; } = string.Empty;
    [Required] public MailTypeViewModel Type { get; set; } = MailTypeViewModel.Unknown;
    public List<IFormFile> Attachments { get; set; } = new List<IFormFile>();
}