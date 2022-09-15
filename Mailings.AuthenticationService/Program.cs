using Mailings.AuthenticationService.Core.Extensions;
using Mailings.AuthenticationService.Data.Extensions;
using Mailings.AuthenticationService.Extensions;
using Mailings.AuthenticationService.Shared.Extensions;
using Mailings.AuthenticationService.Shared.ServiceConstants;

var builder = WebApplication.CreateBuilder();
var services = builder.Services;
var config = builder.Configuration;

//Setup static data and configurations

config.ConfigureStaticIdentityPrivateData();
config.ConfigureStaticIdentityClients();
services.ConfigureAdminCredentials(optionsFrom: config);
services.ConfigureRouteOptions();

//Setup DI Container

services.AddIdentityServerDatabase(optionsFrom: config); // Database
services.AddDbInitializer(); // Database Initializer
services.AddPasswordGenerator(); // Password generator
services.AddClaimProvider(); // Claim provider
services.AddResponseFactory(); // Response factory

//Setup framework features

services.AddControllersWithViews();
services.AddAndConfigureCors(); // Cors
services.AddAndConfigureIdentityServer(); // Identity server
services.AddAndConfigureSwashbuckle(); // Open API
services.AddAuthorization();

//Build application
var app = builder.Build();

//Seeding
app.Services.SeedData();

//Middleware configuration

if (app.Environment.IsDevelopment())
{
    //Development
    app.UseDeveloperErrorHandler(); //error handler
    app.UseSwaggerWithDefaultOptions(); //swagger
    app.UseCors(CorsPolicyConstants.AnyPolicy);
}
else
{
    //Staging, Production
    app.UseDefaultOnPageErrorHandler(); //error handler
    app.UseCors(CorsPolicyConstants.DefaultPolicy);
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseIdentityServer();

app.UseControllerEndpoints(); //Endpoints

app.Run();