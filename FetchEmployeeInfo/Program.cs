using FetchEmployeeInfo.Services;
using FetchEmployeeInfoService;
using JwtAuthenticationManager;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCustomJwtAuthentication();

IConfiguration configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();

/* Database Context Dependency Injection */
string dbConnection = configuration["ConnectionStrings:dbConnection"];

builder.Services.AddDbContext<EmployeeDbContext>(opt => opt.UseSqlServer(dbConnection));
builder.Services.AddScoped<IFetchDataService, FetchDataService>();
/* ===================================== */

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();