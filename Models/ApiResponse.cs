namespace WeatherApiProject.Models;

public class ApiResponse<T>(bool isSuccess, string message, T? data)
{
  public bool IsSuccess { get; set; } = isSuccess;
  public string Message { get; set; } = message;
  public T? Data { get; set; } = data;
}