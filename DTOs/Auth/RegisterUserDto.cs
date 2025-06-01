using System.ComponentModel.DataAnnotations;

namespace WeatherApiProject.DTOs
{
  public class RegisterUserDto
  {
    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public string Username { get; set; } = default!;

    [Required]
    [MinLength(6)]
    [MaxLength(20)]
    public string Password { get; set; } = default!;

    [MinLength(3)]
    [MaxLength(20)]
    public string FirstName { get; set; } = default!;

    [MinLength(3)]
    [MaxLength(20)]
    public string LastName { get; set; } = default!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;
  }
}