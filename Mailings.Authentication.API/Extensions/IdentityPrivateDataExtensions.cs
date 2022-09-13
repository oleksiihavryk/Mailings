using Mailings.Authentication.Shared.StaticData;

namespace Mailings.Authentication.API.Extensions;
/// <summary>
///     Class of configuration of IdentityPrivateData static class.
/// </summary>
public static class IdentityPrivateDataExtensions
{
    /// <summary>
    ///     Extension method of configuration of IdentityPrivateData static class.
    /// </summary>
    /// <param name="config">
    ///     Configuration class (access to appsettings.json file)
    /// </param>
    public static void SetupIdentityPrivateData(this IConfiguration config)
    {
        var clientPrivateData = config.GetSection("ClientsPrivateData");
        IdentityPrivateData.SetupClientData(opt =>
        {
            var authData = clientPrivateData.GetSection("Authentication");
            opt.ClientId = authData["ClientId"];
            opt.ClientSecret = authData["ClientSecret"];
            opt.Scopes = authData.GetSection("Scopes").Get<string[]>().ToList();
        }, IdentityClient.Authentication);
        IdentityPrivateData.SetupClientData(opt =>
        {
            var authData = clientPrivateData.GetSection("Resources");
            opt.ClientId = authData["ClientId"];
            opt.ClientSecret = authData["ClientSecret"];
            opt.Scopes = authData.GetSection("Scopes").Get<string[]>().ToList();
        }, IdentityClient.Resources);
        IdentityPrivateData.SetupClientData(opt =>
        {
            var authData = clientPrivateData.GetSection("WebUser");
            opt.ClientId = authData["ClientId"];
            opt.ClientSecret = authData["ClientSecret"];
            opt.Scopes = authData.GetSection("Scopes").Get<string[]>().ToList();
        }, IdentityClient.WebUser);
        IdentityPrivateData.FullAccessScopeName = clientPrivateData["FullAccessScopeName"];
        IdentityPrivateData.WriteDefaultScopeName = clientPrivateData["WriteDefaultScopeName"];
        IdentityPrivateData.ReadDefaultScopeName = clientPrivateData["ReadDefaultScopeName"];
        IdentityPrivateData.ReadSecuredScopeName = clientPrivateData["ReadSecuredScopeName"];
        IdentityPrivateData.ResourcesApiResource = clientPrivateData["ResourcesApiResource"];
    }
}