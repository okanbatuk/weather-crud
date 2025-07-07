using WeatherApiProject.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Custom services & config
builder.Services.AddJsonOptions();
builder.Services.AddSwagger();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationServices();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCustomDbContext(builder.Configuration);

var app = builder.Build();

app.UseExceptionMiddleware();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerDocumentation();

app.MapControllers();
app.Run();
