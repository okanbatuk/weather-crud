using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WeatherApiProject.Helpers;
using WeatherApiProject.Models;
using Microsoft.Extensions.Options;

namespace WeatherApiProject.Services;

public class TokenService(IOptions<JwtSettings> settings)
{
  private readonly JwtSettings _settings = settings.Value;

  public string GenerateToken(User user)
  {
    var claims = new List<Claim>
    {
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Role, user.Role.ToString())
        };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        _settings.Issuer,
        _settings.Audience,
        claims,
        expires: DateTime.UtcNow.AddMinutes(_settings.ExpirationMinutes),
        signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}
