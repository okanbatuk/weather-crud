using Microsoft.EntityFrameworkCore;
using WeatherApiProject.Models;

namespace WeatherApiProject.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<User>()
      .Property(u => u.Role)
      .HasConversion<string>()
      .IsRequired();
  }
  public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();
  public DbSet<User> Users => Set<User>();

}