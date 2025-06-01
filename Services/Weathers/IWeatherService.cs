using WeatherApiProject.Models;
using WeatherApiProject.DTOs.Weathers;

namespace WeatherApiProject.Services.Weathers
{
    public interface IWeatherService : IService<WeatherForecast, UpdateWeatherDto>
    {
        Task<bool> Add(WeatherForecast forecast);
        Task DeleteAll();
    }
}