using System.ComponentModel.DataAnnotations;

namespace Mailings.Web.ViewModels;
public sealed class GeneratedAccountViewModel
{
    [Required] public string Email { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    [Required] public string UserName { get; set; } = string.Empty;
}