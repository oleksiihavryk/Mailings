using Mailings.Web.API.Extensions;
using static Mailings.Web.Api.ComponentOptions;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var services = builder.Services;

config.SetupStaticData();

services.Configure<RouteOptions>(RouteOptions);

services.ConfigureDIContainer();

services.AddMvc(MvcOptions);

services.AddHttpClient();

services.AddAuthentication(AuthenticationOptions)
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", OidcAuthOptions);
services.AddAuthorization(AuthorizationOptions);

var app = builder.Build();

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseStatusCodePages();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseMvc(MvcRouteOptions);

app.Run();