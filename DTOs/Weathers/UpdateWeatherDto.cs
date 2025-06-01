using System.ComponentModel.DataAnnotations;

namespace WeatherApiProject.DTOs.Weathers
{
  public class UpdateWeatherDto
  {
    [DataType(DataType.Date)]
    public DateOnly? Date { get; set; } = default!;

    [Range(-100, 100, ErrorMessage = "Temperature must be between -100 and 100")]
    public int? TemperatureC { get; set; } = default!;

    [MinLength(3)]
    [MaxLength(20)]
    public string? Summary { get; set; } = default!;
  }
}