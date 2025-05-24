using Microsoft.AspNetCore.Mvc;
using WeatherApiProject.Models;
using WeatherApiProject.Services;
using WeatherApiProject.Helpers;

namespace WeatherApiProject.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(TokenService tokenService) : ControllerBase
{
  private static readonly List<User> _users =
    [
        new User { Id = 1, Username = "john", Password = "1234",Role= "Admin" },
        new User { Id = 2, Username = "jane", Password = "1234",Role= "User" }
    ];

  [HttpPost("login")]
  public IActionResult Login([FromBody] User credentials)
  {
    var user = _users.FirstOrDefault(u =>
        u.Username == credentials.Username &&
        u.Password == credentials.Password);

    if (user == null)
      return Unauthorized(ApiResponseHelper.Fail("Invalid credentials."));

    var token = tokenService.GenerateToken(user);
    return Ok(ApiResponseHelper.Success(token, "Login successful."));
  }
}
