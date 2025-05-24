using WeatherApiProject.Models;

namespace WeatherApiProject.Helpers;

public static class ApiResponseHelper
{
  public static ApiResponse<T> Success<T>(T data, string message = "Success")
  {
    return new ApiResponse<T>(
      true,
      message,
      data
    );
  }
  public static ApiResponse<object> Fail(string message = "Failed")
  {
    return new ApiResponse<object>(
      false,
      message,
      null
    );
  }
}