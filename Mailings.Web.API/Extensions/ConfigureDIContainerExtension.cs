using Mailings.Web.API.Filters;
using Mailings.Web.Services;
using Mailings.Web.Services.Core;
using Mailings.Web.Shared.Cloner;

namespace Mailings.Web.API.Extensions;
internal static class ConfigureDIContainerExtension
{
    public static void ConfigureDIContainer(this IServiceCollection services)
    {
        ConfigureCommon(services);
        ConfigureServicesCore(services);
        ConfigureServices(services);
        ConfigureFilters(services);
    }

    private static void ConfigureFilters(IServiceCollection services)
    {
        services.AddScoped<MailsUserSecuredServiceFilter>();
        services.AddScoped<MailingsUserSecuredServiceFilter>();
    }
    private static void ConfigureCommon(IServiceCollection services)
    {
        services.AddTransient<IDeepCloner, JsonDeepCloner>();
    }
    private static void ConfigureServicesCore(IServiceCollection services)
    {
        services.AddScoped<IAuthServiceRequestMode, AuthenticationRequestMode>();
        services.AddScoped<AuthenticationService>();
        services.AddScoped<ResourceService>();
    }
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IHtmlMailsResourceService, HtmlMailsResourceService>();
        services.AddScoped<ITextMailsResourceService, TextMailsResourceService>();
        services.AddScoped<IMailingGroupsResourceService, MailingGroupsResourceService>();
        services.AddScoped<IMailingsSenderResourceService, MailingsSenderResourceService>();
        services.AddScoped<IBetaTestAuthenticationService, BetaTestAuthenticationService>();
        services.AddScoped<IAccountAuthenticationService, AccountAuthenticationService>();
    }
}