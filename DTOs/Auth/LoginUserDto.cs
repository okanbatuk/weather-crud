using System.ComponentModel.DataAnnotations;

namespace WeatherApiProject.DTOs;

public class LoginUserDto
{
  [Required]
  [MinLength(3)]
  [MaxLength(20)]
  public string Username { get; set; } = default!;

  [Required]
  [MinLength(6)]
  [MaxLength(20)]
  public string Password { get; set; } = default!;
}