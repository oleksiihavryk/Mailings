using Mailings.AuthenticationService.Data.ClaimProvider;
using Mailings.AuthenticationService.Data.DbContexts;
using Mailings.AuthenticationService.Data.DbInitializer;
using Mailings.AuthenticationService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mailings.AuthenticationService.Data.Extensions;
/// <summary>
///     Class for program data configuration
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    ///     Adding database for containing Identity Server data
    /// </summary>
    /// <param name="services">
    ///     Provider of DI Container for add and configure database into it
    /// </param>
    /// <param name="optionsFrom">
    ///     Provider of options from appsettings.json
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddIdentityServerDatabase(
        this IServiceCollection services,
        IConfiguration optionsFrom)
    {
        services.AddDbContext<IdentityServerDbContext>(opt =>
            opt.UseSqlServer(
                optionsFrom.GetConnectionString("Identity")));
        return services;
    }
    /// <summary>
    ///     Method for seeding data into application
    /// </summary>
    /// <param name="services">Service collection of build application provide DI Container</param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceProvider SeedData(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        initializer.InitializeAsync().GetAwaiter().GetResult();
        return services;
    }
    /// <summary>
    ///     Adding database initializer into system
    /// </summary>
    /// <param name="services">
    ///     Provider of DI Container for adding database initializer into it
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddDbInitializer(this IServiceCollection services)
    {
        services.AddScoped<IDbInitializer, IdentityDbInitializer>();
        return services;
    }
    /// <summary>
    ///     Adding a claim provider to DI Container
    /// </summary>
    /// <param name="services">
    ///     Provider of DI Container for adding claim provider into it
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddClaimProvider(this IServiceCollection services)
    {
        services.AddScoped<IClaimProvider<User>, UserClaimProvider>();
        return services;
    }
}