using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherApiProject.Data;
using WeatherApiProject.DTOs;
using WeatherApiProject.Helpers;
using WeatherApiProject.Models;

namespace WeatherApiProject.Services.Auth;

public class AuthServices(AppDbContext context) : IAuthServices
{
  private readonly AppDbContext _context = context;

  public async Task<User?> Login(LoginUserDto loginUserDto) => await _context.Users.FirstOrDefaultAsync(u => u.Username == loginUserDto.Username && u.Password == loginUserDto.Password);

  public async Task<bool> Register(RegisterUserDto registerUserDto)
  {
    var existing = await _context.Users.FirstOrDefaultAsync(u => u.Username == registerUserDto.Username);
    if (existing != null) return false;

    _context.Users.Add(new User
    {
      Username = registerUserDto.Username,
      Password = registerUserDto.Password,
      Email = registerUserDto.Email,
      FirstName = registerUserDto.FirstName,
      LastName = registerUserDto.LastName
    });
    _context.SaveChanges();
    return true;
  }
}