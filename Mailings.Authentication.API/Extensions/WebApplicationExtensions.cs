using Mailings.Authentication.Data.DbInitializer;

namespace Mailings.Authentication.API.Extensions;
/// <summary>
///     Class for providing new extension methods into WebApplication class.
/// </summary>
internal static class WebApplicationExtensions
{
    public static void SeedData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        initializer.InitializeAsync().GetAwaiter().GetResult();
    }
}
