using Microsoft.AspNetCore.Mvc;
using WeatherApiProject.Services.Weathers;
using WeatherApiProject.Models;
using Microsoft.AspNetCore.Authorization;
using WeatherApiProject.Helpers;
using WeatherApiProject.DTOs.Weathers;

namespace WeatherApiProject.Controllers.WeatherForecasts;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(IWeatherService service) : ControllerBase
{
  private readonly IWeatherService _service = service;

  [Authorize(Roles = "User, Admin")]
  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var data = await _service.GetAll();
    if (data is null || data.Count == 0) return NotFound(ApiResponseHelper.Fail("No data found."));
    return Ok(ApiResponseHelper.Success(data));
  }

  [Authorize(Roles = "User, Admin")]
  [HttpGet("{id:int}")]
  public async Task<IActionResult> GetById(int id)
  {
    var data = await _service.GetById(id);
    return data is not null ? Ok(ApiResponseHelper.Success(data)) : NotFound(ApiResponseHelper.Fail("Data not found."));
  }

  [Authorize(Roles = "Admin")]
  [HttpPost]
  public async Task<IActionResult> Add(WeatherForecast forecast)
  {
    var added = await _service.Add(forecast);
    return added
      ? CreatedAtAction(nameof(GetById), new { id = forecast.Id }, forecast)
      : Conflict(ApiResponseHelper.Fail("Data already exists."));
  }

  [HttpPut("{id:int}")]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> Update(int id, UpdateWeatherDto updateWeatherDto)
  {
    var updated = await _service.Update(id, updateWeatherDto);
    return updated ? Ok() : NotFound(ApiResponseHelper.Fail("Data not found."));
  }

  [Authorize(Roles = "Admin")]
  [HttpDelete("{id:int}")]
  public async Task<IActionResult> Delete(int id)
  {
    var deleted = await _service.Delete(id);
    return deleted ? NoContent() : NotFound(ApiResponseHelper.Fail("Data not found."));
  }

  [HttpDelete("all")]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> DeleteAll()
  {
    await _service.DeleteAll();
    return NoContent();
  }
}
