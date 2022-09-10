using System.ComponentModel.DataAnnotations;
using Mailings.Web.API.Controllers;

namespace Mailings.Web.API.ViewModels;
public class MailingViewModel
{
    [Required] public Guid Id { get; set; } = Guid.Empty;
    [Required] public MailingMailViewModel Mail { get; set; } = new MailingMailViewModel();
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public List<string> To { get; set; } = new List<string>();
}