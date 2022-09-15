using System.ComponentModel.DataAnnotations;

namespace Mailings.Authentication.ViewModels;
/// <summary>
///     View model of login process
/// </summary>
public sealed class LoginViewModel
{
    /// <summary>
    ///     Username string
    /// </summary>
    [Required]
    [Display(Name = "Username")]
    public string? Username { get; set; }
    /// <summary>
    ///     User password
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    /// <summary>
    ///     Url for returning to page from which authentication process was been invoked
    /// </summary>
    public string ReturnUrl { get; set; } = string.Empty;
}