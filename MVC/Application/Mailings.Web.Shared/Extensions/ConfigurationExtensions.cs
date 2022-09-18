using Mailings.Web.Shared.StaticData;
using Microsoft.Extensions.Configuration;

namespace Mailings.Web.Shared.Extensions;
/// <summary>
///     Application configurations
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    ///     Setup servers static data (authentication and resource services)
    /// </summary>
    /// <param name="config">
    ///     Application configuration provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IConfiguration SetupServersStaticData(this IConfiguration config)
    {
        var servers = config.GetSection("Servers");
        Servers.Authentication = servers["Authentication"];
        Servers.Resources = servers["Resources"];
        return config;
    }
    /// <summary>
    ///     Setup OpenIdConnect credentials to provide client authentication
    /// </summary>
    /// <param name="config">
    ///     Application configuration provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IConfiguration SetupOidcOptionsStaticData(this IConfiguration config)
    {
        var oidcSettings = config.GetSection("OidcSettings");
        OidcSettings.ClientId = oidcSettings["ClientId"];
        OidcSettings.ClientSecret = oidcSettings["ClientSecret"];
        return config;
    }
    /// <summary>
    ///     Setup authentication for services to provide machine to machine authentication
    /// </summary>
    /// <param name="config">
    ///     Application configuration provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IConfiguration SetupServicesAuthenticationStaticData(this IConfiguration config)
    {
        var cpd = config.GetSection("ClientsPrivateData");
        
        IdentityPrivateData.SetupClientData(opt =>
        {
            var auth = cpd.GetSection("Authentication");
            opt.ClientId = auth["ClientId"];
            opt.ClientSecret = auth["ClientSecret"];
            opt.Scopes = auth.GetSection("Scopes").Get<string[]>().ToList();
        }, IdentityClient.Authentication);
        IdentityPrivateData.SetupClientData(opt =>
        {
            var res = cpd.GetSection("Resources");
            opt.ClientId = res["ClientId"];
            opt.ClientSecret = res["ClientSecret"];
            opt.Scopes = res.GetSection("Scopes").Get<string[]>().ToList();
        }, IdentityClient.Resources);

        return config;
    }
}