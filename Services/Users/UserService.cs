using Microsoft.EntityFrameworkCore;
using WeatherApiProject.Data;
using WeatherApiProject.DTOs.Users;

namespace WeatherApiProject.Services.Users;

public class UserService(AppDbContext context) : IUserService
{
  private readonly AppDbContext _context = context;

  public async Task<List<UserDto>> GetAll() => await _context.Users
        .Select(u => new UserDto
        {
          Id = u.Id,
          Username = u.Username,
          FirstName = u.FirstName,
          LastName = u.LastName,
          Email = u.Email
        })
        .ToListAsync();

  public async Task<UserDto?> GetById(int id) => await _context.Users
    .Where(u => u.Id == id)
    .Select(u => new UserDto
    {
      Id = u.Id,
      Username = u.Username,
      FirstName = u.FirstName,
      LastName = u.LastName,
      Email = u.Email
    })
    .FirstOrDefaultAsync();

  public async Task<bool> Update(int id, UpdateUserDto updateUserDto)
  {
    var existing = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    if (existing == null) return false;

    if (!string.IsNullOrEmpty(updateUserDto.Password)) existing.Password = updateUserDto.Password;
    if (!string.IsNullOrEmpty(updateUserDto.FirstName)) existing.FirstName = updateUserDto.FirstName;
    if (!string.IsNullOrEmpty(updateUserDto.LastName)) existing.LastName = updateUserDto.LastName;
    if (!string.IsNullOrEmpty(updateUserDto.Email)) existing.Email = updateUserDto.Email;
    if (updateUserDto.Role.HasValue) existing.Role = updateUserDto.Role.Value;
    await _context.SaveChangesAsync();
    return true;
  }

  public async Task<bool> Delete(int id)
  {
    var existing = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    if (existing == null) return false;

    _context.Users.Remove(existing);
    await _context.SaveChangesAsync();
    return true;
  }
}

