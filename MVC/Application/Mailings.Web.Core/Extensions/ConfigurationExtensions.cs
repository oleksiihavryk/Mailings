using Mailings.Web.Core.Cloner;
using Mailings.Web.Core.Services;
using Mailings.Web.Core.Services.Core;
using Mailings.Web.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Mailings.Web.Core.Extensions;
/// <summary>
///     Application configuration
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    ///     Adding into DI container deep cloner service
    /// </summary>
    /// <param name="services">
    ///     Application DI container provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddDeepCloner(this IServiceCollection services)
        => services.AddTransient<IDeepCloner, JsonDeepCloner>();
    /// <summary>
    ///     Adding into DI container servers services
    /// </summary>
    /// <param name="services">
    ///     Application DI container provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddServersServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthServiceRequestMode, AuthenticationRequestMode>();
        services.AddScoped<AuthenticationService>();
        services.AddScoped<ResourceService>();
        services.AddScoped<IHtmlMailsResourceService, HtmlMailsResourceService>();
        services.AddScoped<ITextMailsResourceService, TextMailsResourceService>();
        services.AddScoped<IMailingGroupsResourceService, MailingGroupsResourceService>();
        services.AddScoped<IMailingsSenderResourceService, MailingsSenderResourceService>();
        services.AddScoped<IBetaTestAuthenticationService, BetaTestAuthenticationService>();
        services.AddScoped<IAccountAuthenticationService, AccountAuthenticationService>();
        return services;
    }
}