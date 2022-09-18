using Mailings.Web.Core.Extensions;
using Mailings.Web.Extensions;
using Mailings.Web.Shared.Extensions;

var builder = WebApplication.CreateBuilder();
var config = builder.Configuration;
var services = builder.Services;

//Setup static data and configurations
config.SetupServersStaticData(); // resource and authentication servers
config.SetupOidcOptionsStaticData(); // OpendIdConnect options
config.SetupServicesAuthenticationStaticData(); // identity private data (for services authentication)
services.SetupRouteOptions(); // route options

//Setup DI Container
services.AddServiceFilters(); // standard service filters
services.AddDeepCloner(); // deep cloner IDeepCloner
services.AddServersServices(); // interfaces for connect with authentication and resource service

//Setup framework features
services.AddMvcWithDefaultOptions(); // mvc with default options
services.AddHttpClient();
services.AddDefaultServiceAuthentication(); // configured to interact with authentication service
services.AddDefaultServiceAuthorization(); // configured to interact with authentication service

var app = builder.Build();

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseStatusCodePages();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseDefaultExceptionHandler(); // exception handler by /error route
}

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseMvcWithConfiguredApplicationRoutes(); // already configured routes for application

app.Run();