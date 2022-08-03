using Mailings.Resources.API.Dto;
using Mailings.Resources.API.Extensions;
using Mailings.Resources.Data.DbContexts;
using Mailings.Resources.Data.Repositories;
using Mailings.Resources.Domen;
using Mailings.Resources.Domen.MailingService;
using Mailings.Resources.Shared.StaticData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

// Add services to the container.
var serversConfig = config.GetSection("Servers");
Servers.Authentication = serversConfig["Authentication"];

services.Configure<MailSettings>(config.GetSection("MailSettings"));

services.AddControllers();

services.AddHttpClient();
services.AddDbContext<CommonResourcesDbContext>(opt
    => opt.UseSqlServer(
        config.GetConnectionString("Resources")));

services.AddSingleton<IResponseFactory, ResponseFactory>();

services.AddScoped<IMailingService, MailingService>();

services.AddScoped<IHistoryNotesRepository, HistoryNotesRepository>();
services.AddScoped<IHtmlMailsRepository, HtmlMailsRepository>();
services.AddScoped<ITextMailsRepository, TextMailsRepository>();
services.AddScoped<IMailingGroupsRepository, MailingGroupsRepository>();

services.AddCors(opt =>
{
    opt.AddPolicy("Any", config =>
    {
        config.AllowAnyHeader();
        config.AllowAnyMethod();
        config.AllowAnyOrigin();
    });
});

services.AddAutoMapper();

services.AddGlobalExceptionHandler();

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.Authority = Servers.Authentication;
        opt.Audience = "_resourceServer";
        opt.IncludeErrorDetails = true;

        if (builder.Environment.IsDevelopment())
            opt.RequireHttpsMetadata = false;
    });

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Mailings",
        Version = "v1",
    });
    opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows()
        {
            AuthorizationCode = new OpenApiOAuthFlow()
            {
                AuthorizationUrl = new Uri(Servers.Authentication + "/connect/authorize"),
                TokenUrl = new Uri(Servers.Authentication + "/connect/token"),
                Scopes = new Dictionary<string, string>()
                {
                    ["readSecured_resourceServer"] = 
                        "Scope for read important and secured data on server side",
                    ["writeDefault_resourceServer"] = 
                        "Scope for write default data on server side"
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

// Configure the HTTP request pipeline.

app.UseGlobalExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "My API V1");
        opt.DocumentTitle = "Mailings API";
        opt.HeadContent = "Mailings API";
        opt.RoutePrefix = string.Empty;
    });
}

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("Any");

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();