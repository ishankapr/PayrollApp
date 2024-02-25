using JwtAuthHandler;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var app = builder.Build();

AppSettingsHelper.AppSettingsConfigure(app.Services.GetRequiredService<IConfiguration>());
app.Run();