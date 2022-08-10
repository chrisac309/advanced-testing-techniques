using AdvancedTestingTechniques.Repos;
using AdvancedTestingTechniques.Services;
using AdvancedTestingTechniques.Services.Humidity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency injection
builder.Services.AddScoped<IWeatherReportService, WeatherReportService>();
builder.Services.AddScoped<ITemperatureService, TemperatureService>();
builder.Services.AddScoped<IRadarService, RadarService>();
builder.Services.AddScoped<IHumidityService, HumidityService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IWeatherRepo, WeatherRepo>();
builder.Services.AddScoped<IHumidityReader, HumidityReader>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
