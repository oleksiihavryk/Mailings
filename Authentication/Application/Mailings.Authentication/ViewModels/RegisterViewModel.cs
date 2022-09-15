using System.ComponentModel.DataAnnotations;

namespace Mailings.Authentication.ViewModels;

public sealed class RegisterViewModel
{
    /// <summary>
    ///     Username string
    /// </summary>
    [Required]
    [Display(Name = "Username")]
    public string Username { get; set; } = string.Empty;
    /// <summary>
    ///     User password 
    /// </summary>
    [DataType("Password")] 
    [Required] 
    public string Password { get; set; } = string.Empty;
    /// <summary>
    ///     User password confirmation
    /// </summary>
    [DataType(DataType.Password)]
    [Required]
    [Display(Name = "Confirm password")]
    public string PasswordConfirmation { get; set; } = string.Empty;
    /// <summary>
    ///     User first name
    /// </summary>
    [Required]
    [Display(Name = "First name")]
    public string FirstName { get; set; } = string.Empty;
    /// <summary>
    ///     User last name
    /// </summary>
    [Required]
    [Display(Name = "Last name")]
    public string LastName { get; set; } = string.Empty;
    /// <summary>
    ///     User email address
    /// </summary>
    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Account email address")]
    public string Email { get; set; } = string.Empty;
    /// <summary>
    ///     Url for returning to page from which authentication process was been invoked
    /// </summary>
    public string ReturnUrl { get; set; } = string.Empty;
}