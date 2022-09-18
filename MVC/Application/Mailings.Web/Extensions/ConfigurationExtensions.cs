using IdentityModel;
using Mailings.Web.Core.Cloner;
using Mailings.Web.Filters;
using Mailings.Web.Shared.StaticData;
using Mailings.Web.Shared.SystemConstants;

namespace Mailings.Web.Extensions;
/// <summary>
///     Application configurations
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    ///     Setup route options in service
    /// </summary>
    /// <param name="services">
    ///     Application features provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection SetupRouteOptions(this IServiceCollection services)
        => services.Configure<RouteOptions>(opt =>
        {
            opt.AppendTrailingSlash = true;
            opt.LowercaseUrls = true;
        });
    /// <summary>
    ///     Adding into DI container custom filters
    /// </summary>
    /// <param name="services">
    ///     Application DI container provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddServiceFilters(this IServiceCollection services)
    {
        services.AddScoped<MailsUserSecuredServiceFilter>();
        services.AddScoped<MailingsUserSecuredServiceFilter>();
        return services;
    }
    /// <summary>
    ///     Adding MVC services with default options
    /// </summary>
    /// <param name="services">
    ///     Application features provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddMvcWithDefaultOptions(this IServiceCollection services)
    {
        services.AddMvc(opt => opt.EnableEndpointRouting = false);
        return services;
    }
    /// <summary>
    ///     Adding support of client authentication with default options
    /// </summary>
    /// <param name="services">
    ///     Application features provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddDefaultServiceAuthentication(
        this IServiceCollection services)
    {
        services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = AuthenticationSchemes.CookiesScheme;
                opt.DefaultAuthenticateScheme = AuthenticationSchemes.OidcScheme;
                opt.DefaultChallengeScheme = AuthenticationSchemes.OidcScheme;
            })
            .AddCookie(AuthenticationSchemes.CookiesScheme)
            .AddOpenIdConnect(AuthenticationSchemes.OidcScheme, opt =>
            {
                opt.Authority = Servers.Authentication;
                opt.ResponseType = OidcConstants.ResponseTypes.Code;

                opt.ClientId = OidcSettings.ClientId;
                opt.ClientSecret = OidcSettings.ClientSecret;

                opt.UsePkce = true;

                opt.SaveTokens = true;

                opt.GetClaimsFromUserInfoEndpoint = true;

                opt.Scope.Clear();
                opt.Scope.Add(OidcConstants.StandardScopes.Email);
                opt.Scope.Add(OidcConstants.StandardScopes.OpenId);
                opt.Scope.Add(OidcConstants.StandardScopes.Profile);
            });
        return services;
    }
    /// <summary>
    ///     Adding support of client authorization with default options
    /// </summary>
    /// <param name="services">
    ///     Application features provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddDefaultServiceAuthorization(
        this IServiceCollection services)
        => services.AddAuthorization(opt => 
        {
            opt.AddPolicy(AuthorizationPolicyConstants.Admin, config =>
            {
                config.RequireRole(Roles.Administrator.ToString());
                config.RequireAuthenticatedUser();
            });
            opt.AddPolicy(AuthorizationPolicyConstants.BetaTest, config =>
            {
                config.RequireRole(Roles.BetaTester.ToString(), Roles.Administrator.ToString());
                config.RequireAuthenticatedUser();
            });
        });
}