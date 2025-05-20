using Microsoft.EntityFrameworkCore;
using WeatherApiProject.Models;

namespace WeatherApiProject.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

  public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();
}