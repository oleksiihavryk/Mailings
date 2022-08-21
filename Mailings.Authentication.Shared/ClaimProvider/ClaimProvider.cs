using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace Mailings.Authentication.Shared.ClaimProvider;

public class UserClaimProvider : IClaimProvider<User>
{
    protected readonly UserManager<User> _userManager;
    protected readonly IDictionary<string, Func<User, string>> _claimTypeValue =
        new Dictionary<string, Func<User, string>>()
        {
            [JwtClaimTypes.GivenName] = u => u.FirstName,
            [JwtClaimTypes.FamilyName] = u => u.LastName,
            [JwtClaimTypes.Email] = u => u.Email,
            [JwtClaimTypes.PreferredUserName] = u => u.UserName,
            [JwtClaimTypes.Id] = u => u.Id,
            [JwtClaimTypes.EmailVerified] = u => u.EmailConfirmed.ToString()
        };

    public UserClaimProvider(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public virtual async Task ProvideClaimsAsync(User user, Roles role)
    {
        List<Claim> claims = new List<Claim>();
        
        foreach (var pare in _claimTypeValue)
        {
            var claim = new Claim(pare.Key, pare.Value(user));
            claims.Add(claim);
        }

        //adding role
        var roleClaim = new Claim(JwtClaimTypes.Role, role.ToString());
        claims.Add(roleClaim);

        await _userManager.AddClaimsAsync(user, claims);
    }
    public virtual async Task RevertClaimsAsync(User user)
    {
        var claims = _userManager.GetClaimsAsync(user);
        await _userManager.RemoveClaimsAsync(user, await claims);
    }
}