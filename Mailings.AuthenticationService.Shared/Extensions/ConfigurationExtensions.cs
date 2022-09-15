using Mailings.AuthenticationService.Shared.IdentityData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mailings.AuthenticationService.Shared.Extensions;
/// <summary>
///     Class for program data configuration.
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    ///     Configuration of identity private static data.
    /// </summary>
    /// <param name="config">
    ///     Configuration class (access to appsettings.json file)
    /// </param>
    /// <returns>
    ///     Provide of fluent api.
    /// </returns>
    public static IConfiguration ConfigureStaticIdentityPrivateData(this IConfiguration config)
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
            var authData = clientPrivateData.GetSection("MvcClient");
            opt.ClientId = authData["ClientId"];
            opt.ClientSecret = authData["ClientSecret"];
            opt.Scopes = authData.GetSection("Scopes").Get<string[]>().ToList();
        }, IdentityClient.WebUser);
        IdentityPrivateData.PartialAccessApiResource = clientPrivateData["PartialAccessApiResource"];
        IdentityPrivateData.FullAcessApiResource = clientPrivateData["FullAccessApiResource"];
        IdentityPrivateData.ResourcesApiResource = clientPrivateData["ResourcesApiResource"];
        return config;
    }
    /// <summary>
    ///     Configuration of identity clients static data.
    /// </summary>
    /// <param name="config">
    ///     Configuration class (access to appsettings.json file)
    /// </param>
    /// <returns>
    ///     Provide of fluent api.
    /// </returns>
    public static IConfiguration ConfigureStaticIdentityClients(this IConfiguration config)
    {
        var clients = config.GetSection("Clients");
        IdentityClients.ClientServer = clients["Client"];
        IdentityClients.ResourceServer = clients["Resource"];
        return config;
    }
    /// <summary>
    ///     Configuration of admin credentials provided to IOption class from appsettings.
    /// </summary>
    /// <param name="optionsFrom">
    ///     Configuration class (access to appsettings.json file)
    /// </param>
    /// <param name="services">
    ///     Provider of DI Container for configure admin credentials into it.
    /// </param>
    /// <returns>
    ///     Provide of fluent api.
    /// </returns>
    public static IServiceCollection ConfigureAdminCredentials(
        this IServiceCollection services,
        IConfiguration optionsFrom)
    {
        services.Configure<AdminCredentials>(optionsFrom.GetSection("AdminCredentials"));
        return services;
    }
}