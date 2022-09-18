namespace Mailings.Web.Extensions;
/// <summary>
///     Application middleware configuration
/// </summary>
public static class MiddlewareExtensions
{
    /// <summary>
    ///     Adding custom error handler configured by default
    /// </summary>
    /// <param name="app">
    ///     Application middleware chain provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IApplicationBuilder UseDefaultExceptionHandler(this IApplicationBuilder app)
        => app.UseExceptionHandler("/Error");
    /// <summary>
    ///     Adding custom MVC middleware configured with application routes
    /// </summary>
    /// <param name="app">
    ///     Application middleware chain provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IApplicationBuilder UseMvcWithConfiguredApplicationRoutes(
        this IApplicationBuilder app)
    {
        app.UseMvc(routes =>
        {
            routes.MapRoute(null,
                "admin/generate-account",
                defaults: new { controller = "Admin", action = "GenerateAccount" });
            routes.MapRoute(null,
                "admin/account-is-generated",
                defaults: new { controller = "Admin", action = "AccountIsGenerated" });
            routes.MapRoute(null,
                "mails/all/{page:int:min(0)}",
                defaults: new { controller = "Mails", action = "All" });
            routes.MapRoute(null,
                "mails/delete/{type:required}/{id:required}",
                defaults: new { controller = "Mails", action = "Delete" });
            routes.MapRoute(null,
                "mails/change/{type:required}/{id:required}",
                defaults: new { controller = "Mails", action = "Change" });
            routes.MapRoute(null,
                template: "mailings/more/{id:required}",
                defaults: new { controller = "Mailings", action = "More" });
            routes.MapRoute(null,
                template: "mailings/setup/{id:required}",
                defaults: new { controller = "Mailings", action = "Setup" });
            routes.MapRoute(null,
                template: "mailings/change/{id:required}",
                defaults: new { controller = "Mailings", action = "Change" });
            routes.MapRoute(null,
                template: "mailings/delete/{id:required}",
                defaults: new { controller = "Mailings", action = "Delete" });
            routes.MapRoute(null,
                template: "sender/settings/{id:required}",
                defaults: new { controller = "Sender", action = "Settings" });
            routes.MapRoute(null,
                template: "sender/response",
                defaults: new { controller = "Sender", action = "ShowResponse" });
            routes.MapRoute(null,
                "{controller=Home}/{action=Index}");
        });
        return app;
    }
}