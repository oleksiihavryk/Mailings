using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace Mailings.Authentication.Shared.ClaimProvider;

public class UserClaimProvider : IClaimProvider<User>
{
    protected readonly UserManager<User> _userManager;

    public UserClaimProvider(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public virtual async Task ProvideClaimsAsync(User user, Roles role)
    {
        await _userManager.AddClaimAsync(
            user: user,
            claim: new Claim(
                type: JwtClaimTypes.GivenName,
                value: user.FirstName));
        await _userManager.AddClaimAsync(
            user: user,
            claim: new Claim(
                type: JwtClaimTypes.FamilyName,
                value: user.LastName));
        await _userManager.AddClaimAsync(
            user: user,
            claim: new Claim(
                type: JwtClaimTypes.Email,
                value: user.Email));
        await _userManager.AddClaimAsync(
            user: user,
            claim: new Claim(
                type: JwtClaimTypes.EmailVerified,
                value: user.EmailConfirmed.ToString()));
        await _userManager.AddClaimAsync(
            user: user,
            claim: new Claim(
                type: JwtClaimTypes.Id,
                value: user.Id));
        await _userManager.AddClaimAsync(
            user: user,
            claim: new Claim(
                type: JwtClaimTypes.PreferredUserName,
                value: user.UserName));
        await _userManager.AddClaimAsync(
            user: user,
            claim: new Claim(
                type: JwtClaimTypes.Role,
                value: role.ToString()));
    }
}