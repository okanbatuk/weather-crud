using WeatherApiProject.Data;
using WeatherApiProject.Models;
using Microsoft.EntityFrameworkCore;

namespace WeatherApiProject.Services
{
  public class WeatherService(AppDbContext context)
  {
    private readonly AppDbContext _context = context;

    public async Task<List<WeatherForecast>> GetAllAsync()
    {
      return await _context.WeatherForecasts.ToListAsync();
    }

    public async Task<WeatherForecast?> GetByDateAsync(DateOnly date)
    {
      return await _context.WeatherForecasts.FirstOrDefaultAsync(w => w.Date == date);
    }

    public async Task<bool> AddAsync(WeatherForecast forecast)
    {
      var existing = await _context.WeatherForecasts.FirstOrDefaultAsync(w => w.Date == forecast.Date);
      if (existing != null) return false;

      _context.WeatherForecasts.Add(forecast);
      await _context.SaveChangesAsync();
      return true;
    }

    public async Task<bool> UpdateAsync(WeatherForecast forecast)
    {

      var existing = await _context.WeatherForecasts.FirstOrDefaultAsync(w => w.Date == forecast.Date);
      if (existing == null) return false;

      existing.TemperatureC = forecast.TemperatureC;
      existing.Summary = forecast.Summary;
      await _context.SaveChangesAsync();
      return true;
    }

    public async Task<bool> DeleteAsync(DateOnly date)
    {
      var existing = await _context.WeatherForecasts.FirstOrDefaultAsync(w => w.Date == date);
      if (existing == null) return false;

      _context.WeatherForecasts.Remove(existing);
      await _context.SaveChangesAsync();
      return true;
    }

    public async Task DeleteAllAsync()
    {
      var all = await _context.WeatherForecasts.ToListAsync();
      _context.WeatherForecasts.RemoveRange(all);
      await _context.SaveChangesAsync();
    }
  }
}