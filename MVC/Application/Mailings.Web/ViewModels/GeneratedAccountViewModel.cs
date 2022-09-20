using System.ComponentModel.DataAnnotations;

namespace Mailings.Web.ViewModels;
/// <summary>
///     View model of generated account data
/// </summary>
public sealed class GeneratedAccountViewModel
{
    /// <summary>
    ///     Email of generated account
    /// </summary>
    [Required] public string Email { get; set; } = string.Empty;
    /// <summary>
    ///     Password of generated account
    /// </summary>
    [Required] public string Password { get; set; } = string.Empty;
    /// <summary>
    ///     Username of generated account
    /// </summary>
    [Required] public string UserName { get; set; } = string.Empty;
}