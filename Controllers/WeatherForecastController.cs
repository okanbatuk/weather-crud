using Microsoft.AspNetCore.Mvc;
using WeatherApiProject.Services.Weathers;
using WeatherApiProject.Models;
using Microsoft.AspNetCore.Authorization;
using WeatherApiProject.Helpers;
using WeatherApiProject.DTOs.Weathers;

namespace WeatherApiProject.Controllers.WeatherForecasts;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(IWeatherService service, ILogger<WeatherForecastController> logger) : ControllerBase
{
  private readonly IWeatherService _service = service;
  private readonly ILogger<WeatherForecastController> _logger = logger;

  [Authorize(Roles = "User, Admin")]
  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var data = await _service.GetAll();
    if (data is null || data.Count == 0)
    {
      _logger.LogWarning("WeatherForecastController.GetAll() executed but NO DATA found.");
      return NotFound(ApiResponseHelper.Fail("No data found."));
    }
    _logger.LogInformation("WeatherForecastController.GetAll() executed.");
    return Ok(ApiResponseHelper.Success(data));
  }

  [Authorize(Roles = "User, Admin")]
  [HttpGet("{id:int}")]
  public async Task<IActionResult> GetById(int id)
  {
    var data = await _service.GetById(id);
    if (data is not null)
    {
      _logger.LogInformation("WeatherForecastController.GetById() executed.");
      return Ok(ApiResponseHelper.Success(data));
    }
    _logger.LogWarning("WeatherForecastController.GetById() executed but NO record found matching this ID {id}.", id);
    return NotFound(ApiResponseHelper.Fail("Data not found."));

  }

  [Authorize(Roles = "Admin")]
  [HttpPost]
  public async Task<IActionResult> Add(WeatherForecast forecast)
  {
    var added = await _service.Add(forecast);
    if (added)
    {
      _logger.LogInformation("WeatherForecastController.Add() executed.");
      return CreatedAtAction(nameof(GetById), new { id = forecast.Id }, forecast);
    }
    _logger.LogWarning("WeatherForecastController.Add() executed but DATA already exists.");
    return Conflict(ApiResponseHelper.Fail("Data already exists."));
  }

  [HttpPut("{id:int}")]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> Update(int id, UpdateWeatherDto updateWeatherDto)
  {
    var updated = await _service.Update(id, updateWeatherDto);
    if (updated)
    {
      _logger.LogInformation("WeatherForecastController.Update() executed.");
      return Ok();
    }
    _logger.LogWarning("WeatherForecastController.Update() executed but NO record found matching this ID {id}.", id);
    return NotFound(ApiResponseHelper.Fail("Data not found."));
  }

  [Authorize(Roles = "Admin")]
  [HttpDelete("{id:int}")]
  public async Task<IActionResult> Delete(int id)
  {
    var deleted = await _service.Delete(id);
    if (deleted)
    {
      _logger.LogInformation("WeatherForecastController.Delete() executed.");
      return NoContent();
    }
    _logger.LogWarning("WeatherForecastController.Delete() executed but NO record found matching this ID {id}.", id);
    return NotFound(ApiResponseHelper.Fail("Data not found."));
  }

  [HttpDelete("all")]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> DeleteAll()
  {
    await _service.DeleteAll();
    _logger.LogInformation("WeatherForecastController.DeleteAll() executed.");
    return NoContent();
  }
}
