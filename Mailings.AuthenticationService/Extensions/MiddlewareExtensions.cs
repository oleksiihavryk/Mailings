namespace Mailings.AuthenticationService.Extensions;
/// <summary>
///     Configuration of middleware
/// </summary>
public static class MiddlewareExtensions
{
    /// <summary>
    ///     Middleware wrapper of on-page error handler 
    /// </summary>
    /// <param name="app">
    ///     Middleware service provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IApplicationBuilder UseDefaultOnPageErrorHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler("/Error");
        return app;
    }
    /// <summary>
    ///     Middleware wrapper of developer error handler
    /// </summary>
    /// <param name="app">
    ///     Middleware service provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IApplicationBuilder UseDeveloperErrorHandler(this IApplicationBuilder app)
    {
        app.UseDeveloperExceptionPage();
        app.UseStatusCodePages();
        return app;
    }
    /// <summary>
    ///     Middleware wrapper of swagger with default options and invoking services
    /// </summary>
    /// <param name="app">
    ///     Middleware service provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IApplicationBuilder UseSwaggerWithDefaultOptions(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(opt =>
        {
            opt.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "My API V1");
            opt.DocumentTitle = "Mailings authentication API";
            opt.HeadContent = "Mailings authentication API";
            opt.RoutePrefix = string.Empty;
        });
        return app;
    }
    /// <summary>
    ///     Middleware wrapper of endpoints setting by controllers
    /// </summary>
    /// <param name="app">
    ///     Middleware service provider
    /// </param>
    /// <returns>
    ///     Returns itself
    /// </returns>
    public static IApplicationBuilder UseControllerEndpoints(this IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        return app;
    }
}