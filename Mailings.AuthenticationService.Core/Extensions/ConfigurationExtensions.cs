using Mailings.AuthenticationService.Core.PasswordGenerator;
using Mailings.AuthenticationService.Core.ResponseFactory;
using PG = Mailings.AuthenticationService.Core.PasswordGenerator;
using RF = Mailings.AuthenticationService.Core.ResponseFactory;
using Microsoft.Extensions.DependencyInjection;

namespace Mailings.AuthenticationService.Core.Extensions;
/// <summary>
///     Class for program data configuration.
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    ///     Adding a password generator into system
    /// </summary>
    /// <param name="services">
    ///     Provider of DI Container for adding password generator into it
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddPasswordGenerator(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordGenerator, PG.PasswordGenerator>();
        return services;
    }
    /// <summary>
    ///     Adding response factory in system
    /// </summary>
    /// <param name="services">
    ///     Provider of DI Container for adding response factory into it
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IServiceCollection AddResponseFactory(this IServiceCollection services)
    {
        services.AddSingleton<IResponseFactory, RF.ResponseFactory>();
        return services;
    }
}