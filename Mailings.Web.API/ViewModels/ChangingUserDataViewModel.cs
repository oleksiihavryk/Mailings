using System.ComponentModel.DataAnnotations;

namespace Mailings.Web.API.ViewModels;
public class ChangingUserDataViewModel
{
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required] 
    public string LastName { get; set; } = string.Empty;
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;
}