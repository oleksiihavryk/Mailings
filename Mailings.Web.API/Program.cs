using IdentityModel;
using Mailings.Web.API.Extensions;
using Mailings.Web.Shared.StaticData;
using Mailings.Web.Shared.SystemConstants;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var services = builder.Services;

//extension methods
config.SetupStaticData();
services.ConfigureDIContainer();

services.Configure<RouteOptions>(opt =>
{
    opt.AppendTrailingSlash = true;
    opt.LowercaseUrls = true;
});

services.AddMvc(opt => opt.EnableEndpointRouting = false);

services.AddHttpClient();

services.AddAuthentication(opt =>
    {
        opt.DefaultScheme = AuthenticationSchemes.CookiesScheme;
        opt.DefaultAuthenticateScheme = AuthenticationSchemes.OidcScheme;
        opt.DefaultChallengeScheme = AuthenticationSchemes.OidcScheme;
    })
    .AddCookie(AuthenticationSchemes.CookiesScheme)
    .AddOpenIdConnect(AuthenticationSchemes.OidcScheme, opt =>
    {
        opt.Authority = Servers.Authentication;
        opt.ResponseType = OidcConstants.ResponseTypes.Code;

        opt.ClientId = OidcSettings.ClientId;
        opt.ClientSecret = OidcSettings.ClientSecret;

        opt.UsePkce = true;

        opt.SaveTokens = true;

        opt.GetClaimsFromUserInfoEndpoint = true;

        opt.Scope.Clear();
        opt.Scope.Add(OidcConstants.StandardScopes.Email);
        opt.Scope.Add(OidcConstants.StandardScopes.OpenId);
        opt.Scope.Add(OidcConstants.StandardScopes.Profile);
    });
services.AddAuthorization(opt =>
{
    opt.AddPolicy(AuthorizationPolicyConstants.Admin, config =>
    {
        config.RequireRole(Roles.Administrator.ToString());
        config.RequireAuthenticatedUser();
    });
    opt.AddPolicy(AuthorizationPolicyConstants.BetaTest, config =>
    {
        config.RequireRole(Roles.BetaTester.ToString(), Roles.Administrator.ToString());
        config.RequireAuthenticatedUser();
    });
});

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
        defaults: new { controller="Mails", action="All"});
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

app.Run();