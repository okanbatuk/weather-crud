using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WeatherApiProject.Services.Users;
using WeatherApiProject.Models;
using WeatherApiProject.Helpers;
using WeatherApiProject.DTOs.Users;

namespace WeatherApiProject.Controllers.Users;

[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
  private readonly IUserService _userService = userService;

  [HttpGet]
  public async Task<IActionResult> GetAllUsers()
  {
    var data = await _userService.GetAll();
    if (data is null || data.Count == 0) return NotFound(ApiResponseHelper.Fail("No data found."));
    return Ok(ApiResponseHelper.Success(data));
  }
  [HttpGet("{id:int}")]
  public async Task<IActionResult> GetById(int id)
  {
    var data = await _userService.GetById(id);
    return data is not null ? Ok(ApiResponseHelper.Success(data)) : NotFound(ApiResponseHelper.Fail("User not found."));
  }
  [HttpPut]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> Update(int id, UpdateUserDto updateUser)
  {
    var updated = await _userService.Update(id, updateUser);
    return updated ? Ok() : NotFound(ApiResponseHelper.Fail("User not found."));
  }
  [HttpDelete]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> Delete(int id)
  {
    var deleted = await _userService.Delete(id);
    return deleted ? NoContent() : NotFound(ApiResponseHelper.Fail("User not found."));
  }
}