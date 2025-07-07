using Microsoft.AspNetCore.Builder;
using WeatherApiProject.Middlewares;

namespace WeatherApiProject.Extensions
{
  public static class ExceptionMiddlewareExtensions
  {
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
    {
      return app.UseMiddleware<ExceptionMiddleware>();
    }
  }
}