using System.ComponentModel.DataAnnotations;

namespace Mailings.Authentication.API.ViewModels;
public class LoginViewModel
{
    [Required]
    [Display(Name = "User name")]
    public string Username { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public string ReturnUrl { get; set; } = string.Empty;
}