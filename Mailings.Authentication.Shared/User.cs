using Microsoft.AspNetCore.Identity;

namespace Mailings.Authentication.Shared; 
public class User : IdentityUser 
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
