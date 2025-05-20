using Microsoft.AspNetCore.Mvc;
using WeatherApiProject.Services;
using WeatherApiProject.Models;

namespace WeatherApiProject.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(WeatherService service) : ControllerBase
{
  private readonly WeatherService _service = service;

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var data = await _service.GetAllAsync();
    if (data is null || data.Count == 0) return NotFound(ApiResponseHelper.Fail("No data found."));
    return Ok(ApiResponseHelper.Success(data));
  }

  [HttpGet("bydate/{date}")]
  public async Task<IActionResult> GetByDate(DateOnly date)
  {
    var data = await _service.GetByDateAsync(date);
    return data is not null ? Ok(ApiResponseHelper.Success(data)) : NotFound(ApiResponseHelper.Fail("Data for date not found."));
  }

  [HttpPost]
  public async Task<IActionResult> Add(WeatherForecast forecast)
  {
    var added = await _service.AddAsync(forecast);
    return added ? CreatedAtAction(nameof(GetByDate), new { date = forecast.Date }, forecast) : Conflict(ApiResponseHelper.Fail("Data for date already exists."));
  }

  [HttpPut("bydate/{date}")]
  public async Task<IActionResult> Update(DateOnly date, WeatherForecast forecast)
  {
    if (date != forecast.Date) return BadRequest("Date mismatch.");
    var updated = await _service.UpdateAsync(forecast);
    return updated ? Ok() : NotFound(ApiResponseHelper.Fail("Data for date not found."));
  }

  [HttpDelete("bydate/{date}")]
  public async Task<IActionResult> Delete(DateOnly date)
  {
    var deleted = await _service.DeleteAsync(date);
    return deleted ? NoContent() : NotFound(ApiResponseHelper.Fail("Data for date not found."));
  }

  [HttpDelete("all")]
  public async Task<IActionResult> DeleteAll()
  {
    await _service.DeleteAllAsync();
    return NoContent();
  }
}
