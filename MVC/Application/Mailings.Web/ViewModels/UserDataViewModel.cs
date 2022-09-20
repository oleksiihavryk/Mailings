using System.ComponentModel.DataAnnotations;

namespace Mailings.Web.ViewModels;
/// <summary>
///     User data view model (using for profile and either...)
/// </summary>
public sealed class UserDataViewModel
{
    //this view model is not using in forms, but it can.
    //so if you want to use this view model in forms just add some attributes
    //for properties.
    /// <summary>
    ///     User name
    /// </summary>
    [Required] public string UserName { get; set; } = string.Empty;
    /// <summary>
    ///     User first name
    /// </summary>
    [Required] public string FirstName { get; set; } = string.Empty;
    /// <summary>
    ///     User last name
    /// </summary>
    [Required] public string LastName { get; set; } = string.Empty;
    /// <summary>
    ///     User email
    /// </summary>
    [Required] public string Email { get; set; } = string.Empty;
    /// <summary>
    ///     User role
    /// </summary>
    [Required] public string Role { get; set; } = string.Empty;
}