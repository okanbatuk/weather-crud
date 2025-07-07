using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WeatherApiProject.Services.Users;
using WeatherApiProject.Models;
using WeatherApiProject.Helpers;
using WeatherApiProject.DTOs.Users;

namespace WeatherApiProject.Controllers.Users;

[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService, ILogger<UserController> logger) : ControllerBase
{
  private readonly IUserService _userService = userService;
  private readonly ILogger<UserController> _logger = logger;

  [HttpGet]
  public async Task<IActionResult> GetAllUsers()
  {
    var data = await _userService.GetAll();
    if (data is null || data.Count == 0)
    {
      _logger.LogWarning("UserController.GetAllUsers() executed but NO DATA found.");
      return NotFound(ApiResponseHelper.Fail("No data found."));
    }
    _logger.LogInformation("UserController.GetAllUsers() executed.");
    return Ok(ApiResponseHelper.Success(data));
  }
  [HttpGet("{id:int}")]
  public async Task<IActionResult> GetById(int id)
  {
    var data = await _userService.GetById(id);
    if (data is not null)
    {
      _logger.LogInformation("UserController.GetById() executed.");
      return Ok(ApiResponseHelper.Success(data));
    }
    _logger.LogWarning("UserController.GetById() executed but No record found matching this ID {id}..", id);
    return NotFound(ApiResponseHelper.Fail("User not found."));
  }
  [HttpPut("{id:int}")]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> Update(int id, UpdateUserDto updateUser)
  {
    var updated = await _userService.Update(id, updateUser);
    if (updated)
    {
      _logger.LogWarning("UserController.Update() executed.");
      return Ok();
    }
    _logger.LogWarning("UserController.Update() executed but No record found matching this ID {id}..", id);
    return NotFound(ApiResponseHelper.Fail("User not found."));
  }
  [HttpDelete("{id:int}")]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> Delete(int id)
  {
    var deleted = await _userService.Delete(id);
    if (deleted)
    {
      _logger.LogInformation("UserController.Delete() executed.");
      return NoContent();
    }
    _logger.LogWarning("UserController.Delete() executed but No record found matching this ID {id}..", id);
    return NotFound(ApiResponseHelper.Fail("User not found."));
  }
}