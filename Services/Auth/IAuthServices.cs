using WeatherApiProject.Models;
using WeatherApiProject.DTOs;

namespace WeatherApiProject.Services.Auth;

public interface IAuthServices
{
  public Task<User?> Login(LoginUserDto loginUserDto);
  public Task<bool> Register(RegisterUserDto registerUserDto);
}