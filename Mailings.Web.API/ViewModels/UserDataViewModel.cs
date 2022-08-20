using System.ComponentModel.DataAnnotations;

namespace Mailings.Web.API.ViewModels;

public sealed class UserDataViewModel
{
    //this view model is not using in forms, but it can.
    //so if you want to use this view model in forms just add some attributes
    //for properties.
    [Required] public string UserName { get; set; } = string.Empty;
    [Required] public string Role { get; set; } = string.Empty;
    [Required] public string FirstName { get; set; } = string.Empty;
    [Required] public string LastName { get; set; } = string.Empty;
    [Required] public string Email { get; set; } = string.Empty;
}