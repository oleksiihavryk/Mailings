using Microsoft.AspNetCore.Identity;

namespace Mailings.Authentication.Domain; 
/// <summary>
///     Default model of user in Identity Server system
/// </summary>
public class User : IdentityUser 
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
