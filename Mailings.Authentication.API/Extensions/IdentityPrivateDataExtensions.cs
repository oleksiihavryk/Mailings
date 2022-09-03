using Mailings.Authentication.Shared.StaticData;

namespace Mailings.Authentication.API.Extensions;
public static class IdentityPrivateDataExtensions
{
    public static void SetupIdentityPrivateData(this IConfiguration config)
    {
        var clientPrivateData = config.GetSection("ClientsPrivateData");
        IdentityPrivateData.SetupClientData(opt =>
        {
            var authData = clientPrivateData.GetSection("Authentication");
            opt.ClientId = authData["ClientId"];
            opt.ClientSecret = authData["ClientSecret"];
            opt.Scopes = authData.GetValue<List<string>>("Scopes");
        }, IdentityClient.Authentication);
        IdentityPrivateData.SetupClientData(opt =>
        {
            var authData = clientPrivateData.GetSection("Resources");
            opt.ClientId = authData["ClientId"];
            opt.ClientSecret = authData["ClientSecret"];
            opt.Scopes = authData.GetValue<List<string>>("Scopes");
        }, IdentityClient.Resources);
        IdentityPrivateData.SetupClientData(opt =>
        {
            var authData = clientPrivateData.GetSection("WebUser");
            opt.ClientId = authData["ClientId"];
            opt.ClientSecret = authData["ClientSecret"];
            opt.Scopes = authData.GetValue<List<string>>("Scopes");
        }, IdentityClient.WebUser);
        IdentityPrivateData.FullAccessScopeName = clientPrivateData["FullAccessScopeName"];
        IdentityPrivateData.WriteDefaultScopeName = clientPrivateData["WriteDefaultScopeName"];
        IdentityPrivateData.ReadDefaultScopeName = clientPrivateData["ReadDefaultScopeName"];
        IdentityPrivateData.ReadSecuredScopeName = clientPrivateData["ReadSecuredScopeName"];
        IdentityPrivateData.ResourcesApiResource = clientPrivateData["ResourcesApiResource"];
    }
}