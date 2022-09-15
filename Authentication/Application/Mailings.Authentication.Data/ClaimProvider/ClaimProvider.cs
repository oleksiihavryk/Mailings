using System.Security.Claims;
using IdentityModel;
using Mailings.Authentication.Domain;
using Microsoft.AspNetCore.Identity;

namespace Mailings.Authentication.Data.ClaimProvider;
/// <summary>
///     Implementation of user claims provider
/// </summary>
public class UserClaimProvider : IClaimProvider<User>
{
    /// <summary>
    ///     User manager which injected with DI Container
    /// </summary>
    protected readonly UserManager<User> _userManager;

    /// <summary>
    ///     Dictionary which contains every claim types which provide to user and
    ///     every user property which claim is attachment
    /// </summary>
    protected virtual IDictionary<string, Func<User, string>> _claimTypeValue { get; } =
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

    /// <summary>
    ///     Providing of claims to user
    /// </summary>
    /// <param name="user">User for which claims is provided</param>
    /// <param name="role">Role which user is have</param>
    /// <returns>
    ///     Task of async operation by providing claims to user
    /// </returns>
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
    /// <summary>
    ///     Reverting all provided claims to user
    /// </summary>
    /// <param name="user">
    ///     User for which claims is reverted
    /// </param>
    /// <returns>
    ///     Task of async operation by reverting claims for user
    /// </returns>
    public virtual async Task RevertClaimsAsync(User user)
    {
        var claims = _userManager.GetClaimsAsync(user);
        await _userManager.RemoveClaimsAsync(user, await claims);
    }
}