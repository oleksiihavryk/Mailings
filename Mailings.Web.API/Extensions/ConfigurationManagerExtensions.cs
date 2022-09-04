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
    }
}