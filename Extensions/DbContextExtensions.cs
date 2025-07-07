using Microsoft.EntityFrameworkCore;
using WeatherApiProject.Data;

namespace WeatherApiProject.Extensions
{
  public static class DbContextExtensions
  {
    public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<AppDbContext>(opt =>
        opt.UseSqlite("Data Source=weather.db"));

      return services;
    }
  }
}