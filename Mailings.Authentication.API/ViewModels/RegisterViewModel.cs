using System.ComponentModel.DataAnnotations;

namespace Mailings.Authentication.API.ViewModels;

public sealed class RegisterViewModel
{
    [Required]
    [Display(Name = "User name")]
    public string Username { get; set; } = string.Empty;
    [DataType("Password")] 
    [Required] 
    public string Password { get; set; } = string.Empty;
    [DataType(DataType.Password)]
    [Required]
    [Display(Name = "Confirm password")]
    public string PasswordConfirmation { get; set; } = string.Empty;
    [Required]
    [Display(Name = "First name")]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    [Display(Name = "Last name")]
    public string LastName { get; set; } = string.Empty;
    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Account email address")]
    public string Email { get; set; } = string.Empty;
    public string ReturnUrl { get; set; } = string.Empty;
}