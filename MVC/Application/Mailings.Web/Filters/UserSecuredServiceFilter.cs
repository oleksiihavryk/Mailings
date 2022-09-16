using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mailings.Web.Filters;
public abstract class UserSecuredServiceFilter : IAsyncAuthorizationFilter
{
    public abstract Task OnAuthorizationAsync(AuthorizationFilterContext context);

    protected string? GetUserId(AuthorizationFilterContext context)
        => (context
                .HttpContext
                .User
                .Identity as ClaimsIdentity)?
            .Claims?
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
            .Value;
}