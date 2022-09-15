using Mailings.Authentication.API.Extensions;
using Mailings.Authentication.API.Infrastructure;
using Mailings.Authentication.Shared;
using Mailings.Authentication.Shared.StaticData;
using Mailings.Authentication.Data.DbContexts;
using Mailings.Authentication.Data.DbInitializer;
using Mailings.Authentication.API;
using Mailings.Authentication.API.ResponseFactory;
using Mailings.Authentication.Shared.ClaimProvider;
using Mailings.Authentication.Shared.PasswordGenerator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder();
var services = builder.Services;
var config = builder.Configuration;

//Configure services

config.SetupIdentityPrivateData();

var clients = config.GetSection("Clients");
IdentityClients.ClientServer = clients["Client"];
IdentityClients.ResourceServer = clients["Resource"];

services.Configure<AdminCredentials>(config.GetSection("AdminCredentials"));

services.Configure<RouteOptions>(opt =>
{
    opt.AppendTrailingSlash = true;
    opt.LowercaseUrls = true;
});

services.AddControllersWithViews();

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
services.AddSingleton<IPasswordGenerator, PasswordGenerator>();
services.AddScoped<IClaimProvider<User>, UserClaimProvider>();
services.AddSingleton<IResponseFactory, ResponseFactory>();

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
    .AddInMemoryClients(IdentityServerStaticData.Clients)
    .AddInMemoryIdentityResources(IdentityServerStaticData.IdentityResources)
    .AddInMemoryApiResources(IdentityServerStaticData.ApiResources)
    .AddDeveloperSigningCredential();

services.AddLocalApiAuthentication();
services.AddAuthorization();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Mailings | Auth",
        Version = "v1",
    });
    opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows()
        {
            ClientCredentials = new OpenApiOAuthFlow()
            {
                AuthorizationUrl = new Uri("https://localhost:7001" + "/connect/authorize"),
                TokenUrl = new Uri("https://localhost:7001" + "/connect/token"),
                Scopes = new Dictionary<string, string>()
                {
                    ["default_authenticationServer"] =
                        "Scope which provides full access to authentication server."
                },
            }
        }
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                },
                Scheme = "oauth2",
                Name = "oauth2",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

//Seeding data
app.SeedData();

//Configure middleware
app.UseExceptionHandler("/Error");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseStatusCodePages();
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "My API V1");
        opt.DocumentTitle = "Mailings authentication API";
        opt.HeadContent = "Mailings authentication API";
        opt.RoutePrefix = string.Empty;
    });
}
app.UseCors("Any");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseIdentityServer();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();