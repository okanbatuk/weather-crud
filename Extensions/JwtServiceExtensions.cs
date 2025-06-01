using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WeatherApiProject.Helpers;

namespace WeatherApiProject.Extensions
{
  public static class JwtServiceExtensions
  {
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
    {
      var jwtSettings = config.GetSection("JwtSettings").Get<JwtSettings>();
      var key = Encoding.UTF8.GetBytes(jwtSettings!.Key);

      services.Configure<JwtSettings>(config.GetSection("JwtSettings"));

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(opt =>
          {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
              ValidateIssuer = true,
              ValidateAudience = true,
              ValidateLifetime = true,
              ValidateIssuerSigningKey = true,
              ValidIssuer = jwtSettings.Issuer,
              ValidAudience = jwtSettings.Audience,
              IssuerSigningKey = new SymmetricSecurityKey(key)
            };
          });

      return services;
    }
  }
}
