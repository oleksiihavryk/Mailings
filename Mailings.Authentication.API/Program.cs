using Mailings.Authentication.API.Extensions;
using Mailings.Authentication.API.Infrastructure;
using Mailings.Authentication.Shared;
using Mailings.Authentication.Shared.StaticData;
using Mailings.Authentication.Data.DbContexts;
using Mailings.Authentication.Data.DbInitializer;
using Mailings.Authentication.API;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();
var services = builder.Services;
var config = builder.Configuration;

//Configure services

var clients = config.GetSection("Clients");
IdentityClients.ClientServer = clients["Client"];
IdentityClients.ResourceServer = clients["Resource"];

services.Configure<RouteOptions>(opt =>
{
    opt.AppendTrailingSlash = true;
    opt.LowercaseUrls = true;
});

services.AddMvc(opt => opt.EnableEndpointRouting = false);

services.AddDbContext<IdentityServerDbContext>(opt =>
    opt.UseSqlServer(
        config.GetConnectionString("Identity")));

services.AddCors(opt =>
{
    opt.AddPolicy("Any", builder =>
    {
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        builder.AllowAnyOrigin();
    });
    opt.AddPolicy("Default", builder =>
    {
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        builder.WithOrigins(IdentityClients.ClientServer, IdentityClients.ResourceServer);
    });
});

services.AddScoped<IDbInitializer, IdentityDbInitializer>();

services.AddIdentity<User, IdentityRole>(opt =>
{
    var passOpt = opt.Password;
    var userOpt = opt.User;

    passOpt.RequireDigit = true;
    passOpt.RequireLowercase = false;
    passOpt.RequireNonAlphanumeric = false;
    passOpt.RequireUppercase = false;
    passOpt.RequiredLength = 8;
    passOpt.RequiredUniqueChars = 4;

    userOpt.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<IdentityServerDbContext>()
    .AddPasswordValidator<DigitsCountPasswordValidator>()
    .AddDefaultTokenProviders();

services.AddIdentityServer(opt =>
    {
        opt.Events.RaiseFailureEvents = true;
        opt.Events.RaiseSuccessEvents = true;
        opt.Events.RaiseInformationEvents = true;
        opt.Events.RaiseErrorEvents = true;
    })
    .AddAspNetIdentity<User>()
    .AddInMemoryApiScopes(IdentityServerStaticData.Scopes)
    .AddInMemoryClients(IdentityServerStaticData
        .GetClients(
            withPkce: !builder.Environment.IsDevelopment()))
    .AddInMemoryIdentityResources(IdentityServerStaticData.IdentityResources)
    .AddInMemoryApiResources(IdentityServerStaticData.ApiResources)
    .AddDeveloperSigningCredential();

var app = builder.Build();

//Seeding data
app.SeedData();

//Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseStatusCodePages();
}
app.UseCors("Any");

app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.UseMvc();

await app.RunAsync();