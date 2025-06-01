using System.ComponentModel.DataAnnotations;
using WeatherApiProject.Enums;

namespace WeatherApiProject.DTOs.Users
{
  public class UpdateUserDto
  {
    [MinLength(3)]
    [MaxLength(20)]
    public string? Username { get; set; }

    [MinLength(6)]
    [MaxLength(20)]
    public string? Password { get; set; }

    [MinLength(3)]
    [MaxLength(20)]
    public string? FirstName { get; set; }

    [MinLength(3)]
    [MaxLength(20)]
    public string? LastName { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    public UserRole? Role { get; set; }
  }
}