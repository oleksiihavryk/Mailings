using Mailings.Web.Shared.StaticData;

namespace Mailings.Web.API.Extensions;
internal static class ConfigurationManagerExtensions
{
    public static void SetupStaticData(this ConfigurationManager config)
    {
        var servers = config.GetSection("Servers");
        Servers.Authentication = servers["Authentication"];
        Servers.Resources = servers["Resources"];

        var oidcSettings = config.GetSection("OidcSettings");
        OidcSettings.ClientId = oidcSettings["ClientId"];
        OidcSettings.ClientSecret = oidcSettings["ClientSecret"];
    }
}