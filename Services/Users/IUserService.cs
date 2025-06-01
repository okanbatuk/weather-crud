using WeatherApiProject.DTOs.Users;

namespace WeatherApiProject.Services.Users
{
  public interface IUserService : IService<UserDto, UpdateUserDto> { }
}