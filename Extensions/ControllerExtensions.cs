using System.Text.Json.Serialization;

namespace WeatherApiProject.Extensions
{
  public static class ControllerExtensions
  {
    public static IServiceCollection AddJsonOptions(this IServiceCollection services)
    {
      services.AddControllers().AddJsonOptions(options =>
      {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
      });

      return services;
    }
  }
}