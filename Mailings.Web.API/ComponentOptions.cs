using IdentityModel;
using Mailings.Web.Shared.StaticData;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.Api;

internal static class ComponentOptions
{
    public static void MvcRouteOptions(IRouteBuilder routes)
    {
        routes.MapRoute(null,
            "admin/generate-account",
            defaults: new { controller="Admin", action = "GenerateAccount"});
        routes.MapRoute(null,
            "admin/account-is-generated",
            defaults: new { controller="Admin", action = "AccountIsGenerated" });
        routes.MapRoute(null,
            "{controller=Home}/{action=Index}");
    }
    public static void AuthorizationOptions(AuthorizationOptions opt)
    {
        opt.AddPolicy("Admin", config =>
        {
            config.RequireRole("Administrator");
            config.RequireAuthenticatedUser();
        });
    }
    public static void OidcAuthOptions(OpenIdConnectOptions opt)
    {
        opt.Authority = Servers.Authentication;
        opt.ClientId = "webUser_Client";
        opt.ClientSecret = "webUser_Secret";
        opt.ResponseType = OidcConstants.ResponseTypes.Code;

        opt.UsePkce = true;

        opt.SaveTokens = true;

        opt.GetClaimsFromUserInfoEndpoint = true;

        opt.Scope.Clear();
        opt.Scope.Add(OidcConstants.StandardScopes.Email);
        opt.Scope.Add(OidcConstants.StandardScopes.OpenId);
        opt.Scope.Add(OidcConstants.StandardScopes.Profile);
    }
    public static void AuthenticationOptions(AuthenticationOptions opt)
    {
        opt.DefaultScheme = "Cookies";
        opt.DefaultAuthenticateScheme = "oidc";
        opt.DefaultChallengeScheme = "oidc";
    }
    public static void MvcOptions(MvcOptions opt)
    {
        opt.EnableEndpointRouting = false;
    }
    public static void RouteOptions(RouteOptions opt)
    {
        opt.AppendTrailingSlash = true;
        opt.LowercaseUrls = true;
    }
}