using System.ComponentModel.DataAnnotations;

namespace Mailings.Authentication.API.ViewModels;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "User name")]
    public string Username { get; set; }
    [DataType("Password")]
    [Required]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    [Required]
    [Display(Name = "Confirm password")]
    public string PasswordConfirmation { get; set; }
    [Required]
    [Display(Name = "First name")]
    public string FirstName { get; set; }
    [Required]
    [Display(Name = "Last name")]
    public string LastName { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Account email address")]
    public string Email { get; set; }
    public string ReturnUrl { get; set; }
}