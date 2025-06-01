using WeatherApiProject.Services;
using WeatherApiProject.Services.Auth;
using WeatherApiProject.Services.Users;
using WeatherApiProject.Services.Weathers;

namespace WeatherApiProject.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
      services.AddScoped<IAuthServices, AuthServices>();
      services.AddScoped<IWeatherService, WeatherService>();
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<TokenService>();

      return services;
    }
  }
}
