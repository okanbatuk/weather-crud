using WeatherApiProject.Enums;

namespace WeatherApiProject.Models;

public class User
{
  public int Id { get; set; }
  public required string Username { get; set; }
  public required string Password { get; set; }
  public required string FirstName { get; set; }
  public required string LastName { get; set; }
  public required string Email { get; set; }
  public UserRole Role { get; set; } = UserRole.User;
}