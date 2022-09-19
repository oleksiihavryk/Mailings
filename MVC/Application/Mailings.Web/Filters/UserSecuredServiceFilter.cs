using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mailings.Web.Filters;
/// <summary>
///     Abstract base class for all user secured filters
/// </summary>
public abstract class UserSecuredServiceFilter : IAsyncAuthorizationFilter
{
    /// <summary>
    ///     Abstract method for check access
    /// </summary>
    /// <param name="context">
    ///     Request context
    /// </param>
    /// <returns>
    ///     Task of async operation by checking access
    /// </returns>
    public abstract Task OnAuthorizationAsync(AuthorizationFilterContext context);

    /// <summary>
    ///     Get identifier of current user
    /// </summary>
    /// <param name="context">
    ///     Request context
    /// </param>
    /// <returns>
    ///     String of user identifier from claims
    /// </returns>
    protected string? GetUserId(AuthorizationFilterContext context)
        => (context
                .HttpContext
                .User
                .Identity as ClaimsIdentity)?
            .Claims?
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
            .Value;
}