using Mailings.Resources.API.Dto;
using Mailings.Resources.API.Extensions;
using Mailings.Resources.Data.DbContexts;
using Mailings.Resources.Data.Repositories;
using Mailings.Resources.Domen;
using Mailings.Resources.Domen.MailingService;
using Mailings.Resources.Shared.StaticData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;

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
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseGlobalExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.DocumentTitle = "Mailings API";
        opt.HeadContent = "Mailings API";
        opt.OAuthConfigObject = new OAuthConfigObject()
        {
            AppName = "Mailings",
        };
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

app.UseEndpoints(config =>
{
    config.MapControllers();
});

app.Run();