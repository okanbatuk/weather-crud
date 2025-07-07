using Microsoft.AspNetCore.Mvc;
using WeatherApiProject.Services;
using WeatherApiProject.Helpers;
using WeatherApiProject.DTOs;
using WeatherApiProject.Services.Auth;

namespace WeatherApiProject.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(IAuthServices authService, TokenService tokenService, ILogger<AuthController> logger) : ControllerBase
{
  private readonly IAuthServices _authService = authService;
  private readonly TokenService _tokenService = tokenService;
  private readonly ILogger<AuthController> _logger = logger;
  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginUserDto credentials)
  {
    _logger.LogDebug("AuthController.Login() executing.");
    var user = await _authService.Login(credentials);

    if (user == null)
    {
      _logger.LogWarning("AuthController.Login() executed but INVALID CREDENTIALS.");
      return Unauthorized(ApiResponseHelper.Fail("Invalid credentials."));
    }

    var token = _tokenService.GenerateToken(user);
    _logger.LogInformation("AuthController.Login() executed and user {username} logged in.", user.Username);
    return Ok(ApiResponseHelper.Success(token, "Login successful."));
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] RegisterUserDto credentials)
  {
    _logger.LogDebug("AuthController.Register() executing.");
    var registered = await _authService.Register(credentials);
    if (!registered)
    {
      _logger.LogWarning("AuthController.Register() executed but Registration failed.");
      return BadRequest(ApiResponseHelper.Fail("Registration failed."));
    }
    _logger.LogInformation("AuthController.Register() executed and user {username} registered.", credentials.Username);
    return Ok(ApiResponseHelper.Success("Registration successful."));
  }
}