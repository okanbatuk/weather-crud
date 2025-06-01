using WeatherApiProject.Data;
using WeatherApiProject.Models;
using Microsoft.EntityFrameworkCore;
using WeatherApiProject.DTOs.Weathers;

namespace WeatherApiProject.Services.Weathers
{
  public class WeatherService(AppDbContext context) : IWeatherService
  {
    private readonly AppDbContext _context = context;

    public async Task<List<WeatherForecast>> GetAll()
    {
      return await _context.WeatherForecasts.ToListAsync();
    }

    public async Task<WeatherForecast?> GetById(int id)
    {
      return await _context.WeatherForecasts.FindAsync(id);
    }

    public async Task<bool> Add(WeatherForecast forecast)
    {
      var existing = await _context.WeatherForecasts.FirstOrDefaultAsync(w => w.Date == forecast.Date);
      if (existing != null) return false;

      _context.WeatherForecasts.Add(forecast);
      await _context.SaveChangesAsync();
      return true;
    }

    public async Task<bool> Update(int id, UpdateWeatherDto updateWeatherDto)
    {

      var existing = await _context.WeatherForecasts.FindAsync(id);
      if (existing == null) return false;
      if (updateWeatherDto.Date.HasValue) existing.Date = updateWeatherDto.Date.Value;
      if (updateWeatherDto.TemperatureC.HasValue) existing.TemperatureC = updateWeatherDto.TemperatureC.Value;
      if (!string.IsNullOrEmpty(updateWeatherDto.Summary)) existing.Summary = updateWeatherDto.Summary;

      await _context.SaveChangesAsync();
      return true;
    }

    public async Task<bool> Delete(int id)
    {
      var existing = await _context.WeatherForecasts.FindAsync(id);
      if (existing == null) return false;

      _context.WeatherForecasts.Remove(existing);
      await _context.SaveChangesAsync();
      return true;
    }

    public async Task DeleteAll()
    {
      var all = await _context.WeatherForecasts.ToListAsync();
      _context.WeatherForecasts.RemoveRange(all);
      await _context.SaveChangesAsync();
    }
  }
}