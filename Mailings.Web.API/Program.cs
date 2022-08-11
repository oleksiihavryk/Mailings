using Mailings.Web.API.Extensions;
using Mailings.Web.Shared.StaticData;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var services = builder.Services;

config.SetupStaticData();

services.Configure<RouteOptions>(opt =>
{
    opt.AppendTrailingSlash = true;
    opt.LowercaseUrls = true;
});

services.AddMvc(opt =>
{
    opt.EnableEndpointRouting = false;
});

services.AddHttpClient();

services.AddAuthentication(opt =>
{
    opt.DefaultScheme = "Cookies";
    opt.DefaultAuthenticateScheme = "oidc";
}).AddCookie("Cookies")
    .AddOpenIdConnect("oidc", opt =>
    {
        opt.Authority = Servers.Authentication;
        opt.ClientId = "webUser_Client";
        opt.ClientSecret = "webUser_Secret";

        opt.UsePkce = true;

        opt.SaveTokens = true;
        opt.GetClaimsFromUserInfoEndpoint = true;

        opt.ClaimActions.MapAll();
    });
services.AddAuthorization();

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

app.UseMvc();

app.Run();
