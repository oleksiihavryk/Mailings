using System.ComponentModel.DataAnnotations;

namespace Mailings.Web.ViewModels;
/// <summary>
///     Form view model by changing user data
/// </summary>
public sealed class ChangingUserDataViewModel
{
    /// <summary>
    ///     New first name of user
    /// </summary>
    [Required] public string FirstName { get; set; } = string.Empty;
    /// <summary>
    ///     New last name of user
    /// </summary>
    [Required] public string LastName { get; set; } = string.Empty;
    /// <summary>
    ///     New email of user
    /// </summary>
    [Required, DataType(DataType.EmailAddress)] 
    public string Email { get; set; } = string.Empty;
}