using System.Security.Claims;

namespace Mailings.Authentication.Shared.ClaimProvider;

public interface IClaimProvider<TUser>
{
    Task ProvideClaimsAsync(TUser user, Roles role);
    Task RevertClaimsAsync(TUser user);
}