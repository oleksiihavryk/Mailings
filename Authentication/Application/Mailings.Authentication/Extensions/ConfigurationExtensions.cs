using Mailings.Authentication.Data.DbContexts;
using Mailings.Authentication.Domain;
using Mailings.Authentication.Infrastructure;
using Mailings.Authentication.Shared.IdentityData;
using Mailings.Authentication.Shared.ServiceConstants;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

namespace Mailings.Authentication.Extensions;

/// <summary>
///     Class for program data configuration
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    ///     Configuration of route options
    /// </summary>
    /// <param name="services">
    ///     Provider of Asp .NET Core features for configure route options
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection ConfigureRouteOptions(
        this IServiceCollection services)
    {
        services.Configure<RouteOptions>(opt =>
        {
            opt.AppendTrailingSlash = true;
            opt.LowercaseUrls = true;
        });
        return services;
    }

    /// <summary>
    ///     Adding and configure Identity Server functionality in system
    /// </summary>
    /// <param name="services">
    ///     Default provider of system services to add in DI Container and configure
    ///     Identity Server
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddAndConfigureIdentityServer(
        this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(opt =>
            {
                var passOpt = opt.Password;
                var userOpt = opt.User;

                passOpt.RequireDigit = true;
                passOpt.RequireLowercase = false;
                passOpt.RequireNonAlphanumeric = false;
                passOpt.RequireUppercase = false;
                passOpt.RequiredLength = 8;
                passOpt.RequiredUniqueChars = 4;

                userOpt.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<IdentityServerDbContext>()
            .AddPasswordValidator<DigitsCountPasswordValidator>()
            .AddDefaultTokenProviders();
        services.AddIdentityServer(opt =>
            {
                opt.Events.RaiseFailureEvents = true;
                opt.Events.RaiseSuccessEvents = true;
                opt.Events.RaiseInformationEvents = true;
                opt.Events.RaiseErrorEvents = true;
            })
            .AddAspNetIdentity<User>()
            .AddInMemoryApiScopes(IdentityServerStaticData.Scopes)
            .AddInMemoryClients(IdentityServerStaticData.Clients)
            .AddInMemoryIdentityResources(IdentityServerStaticData.IdentityResources)
            .AddInMemoryApiResources(IdentityServerStaticData.ApiResources)
            .AddDeveloperSigningCredential();
        services.AddLocalApiAuthentication();

        return services;
    }

    /// <summary>
    ///     Adding and configure CORS policy in system
    /// </summary>
    /// <param name="services">
    ///     Default provider of system services to add in DI Container and configure
    ///     CORS policy in system 
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddAndConfigureCors(this IServiceCollection services)
    {
        services.AddCors(opt =>
        {
            opt.AddPolicy(CorsPolicyConstants.AnyPolicy, builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });
            opt.AddPolicy(CorsPolicyConstants.DefaultPolicy, builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.WithOrigins(IdentityClients.MvcClient, IdentityClients.ResourceServer);
            });
        });
        return services;
    }

    /// <summary>
    ///     Adding swashbuckle swagger into system
    /// </summary>
    /// <param name="services">
    ///     Default provider of system services and DI Container for adding and configure
    ///     swashbuckle in system
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddAndConfigureSwashbuckle(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "Mailings | Auth",
                Version = "v1",
            });
            opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows()
                {
                    ClientCredentials = new OpenApiOAuthFlow()
                    {
                        AuthorizationUrl = new Uri("https://localhost:7001" + "/connect/authorize"),
                        TokenUrl = new Uri("https://localhost:7001" + "/connect/token"),
                        Scopes = new Dictionary<string, string>()
                        {
                            ["default_authenticationServer"] =
                                "Scope which provides full access to authentication server."
                        },
                    }
                }
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        },
                        Scheme = "oauth2",
                        Name = "oauth2",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });
        return services;
    }
}