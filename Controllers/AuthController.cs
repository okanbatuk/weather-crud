using Microsoft.AspNetCore.Mvc;
using WeatherApiProject.Services;
using WeatherApiProject.Helpers;
using WeatherApiProject.DTOs;
using WeatherApiProject.Services.Auth;

namespace WeatherApiProject.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(IAuthServices authService, TokenService tokenService) : ControllerBase
{
  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginUserDto credentials)
  {
    var user = await authService.Login(credentials);

    if (user == null)
      return Unauthorized(ApiResponseHelper.Fail("Invalid credentials."));

    var token = tokenService.GenerateToken(user);
    return Ok(ApiResponseHelper.Success(token, "Login successful."));
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] RegisterUserDto credentials)
  {
    var registered = await authService.Register(credentials);
    return registered ? Ok(ApiResponseHelper.Success("Registration successful.")) : BadRequest(ApiResponseHelper.Fail("Registration failed."));
  }
}
