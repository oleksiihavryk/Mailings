﻿using Mailings.Resources.Middleware;

namespace Mailings.Resources.Extensions;
internal static class GlobalExceptionHandlerExtensions
{
    public static void AddGlobalExceptionHandler(this IServiceCollection services)
        => services.AddScoped<GlobalExceptionHandler>();
    public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        => app.UseMiddleware<GlobalExceptionHandler>();
    
}