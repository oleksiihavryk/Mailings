using Mailings.AuthenticationService.Domain;

namespace Mailings.AuthenticationService.Data.ClaimProvider;
/// <summary>
///     Claim provider service
/// </summary>
/// <typeparam name="TUser">
///     User for which claim provider is created
/// </typeparam>
public interface IClaimProvider<TUser>
    where TUser : User
{
    /// <summary>
    ///     Providing claims for user
    /// </summary>
    /// <param name="user">
    ///     User for which claims is provided
    /// </param>
    /// <param name="role">
    ///     Role of user which he have
    /// </param>
    /// <returns>
    ///     Task of async operation by claims providing
    /// </returns>
    Task ProvideClaimsAsync(TUser user, Roles role);
    /// <summary>
    ///     Reverting provided claims for user
    /// </summary>
    /// <param name="user">
    ///     User for which claims is reverted
    /// </param>
    /// <returns>
    ///     Task of async operation by claims reverting
    /// </returns>
    Task RevertClaimsAsync(TUser user);
}