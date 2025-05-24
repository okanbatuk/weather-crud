using System.Net;
using System.Text.Json;
using WeatherApiProject.Models;

namespace WeatherApiProject.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
  private readonly RequestDelegate _next = next;
  private readonly ILogger<ExceptionMiddleware> _logger = logger;

  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Something went wrong");
      await HandleExceptionAsync(context, ex);
    }
  }

  private static Task HandleExceptionAsync(HttpContext context, Exception ex)
  {
    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    context.Response.ContentType = "application/json";
    var response = new ApiResponse<object>(false, ex.Message, null);
    var result = JsonSerializer.Serialize(response);
    return context.Response.WriteAsync(result);
  }
}