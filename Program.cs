using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WeatherApiProject.Data;
using WeatherApiProject.Extensions;
using WeatherApiProject.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers()
    .AddJsonOptions(opt => opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Custom services & config
builder.Services.AddApplicationServices();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Data Source=weather.db"));

var app = builder.Build();

// Middleware
app.UseMiddleware<ExceptionMiddleware>();

// Auth
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
