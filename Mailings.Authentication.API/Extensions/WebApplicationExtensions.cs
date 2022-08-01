using Mailings.Authentication.Data.DbInitializer;

namespace Mailings.Authentication.API.Extensions;
public static class WebApplicationExtensions
{
    public static void SeedData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        initializer.InitializeAsync().GetAwaiter().GetResult();
    }
}
