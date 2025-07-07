using Microsoft.OpenApi.Models;

namespace WeatherApiProject.Extensions;

public static class SwaggerExtensions
{
  public static IServiceCollection AddSwagger(this IServiceCollection services)
  {
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(opt =>
    {
      opt.SwaggerDoc("v1", new OpenApiInfo
      {
        Title = "Weather API",
        Version = "v1",
        Description = "Weather API with JWT authentication"
      });

      opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
      {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
      });

      opt.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
            {
              Type = ReferenceType.SecurityScheme,
              Id = "Bearer"
            }
          },
          Array.Empty<string>()
        }
      });

    });

    return services;
  }

  public static WebApplication UseSwaggerDocumentation(this WebApplication app)
  {
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }
    return app;
  }
}