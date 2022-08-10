using AdvancedTestingTechniques.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedTestingTechniques.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class WeatherForecastController : ControllerBase
   {

      private readonly ILogger<WeatherForecastController> _logger;
      private readonly IWeatherReportService _weatherService;

      public WeatherForecastController(
         ILogger<WeatherForecastController> logger,
         IWeatherReportService weatherService)
      {
         _logger = logger;
         _weatherService = weatherService;
      }

      [HttpGet("/weekly", Name = "GetWeeklyForecast")]
      public IEnumerable<WeatherForecast> Get([FromQuery] string zipCode)
      {
         _logger.LogInformation("Received request to get weather forecast.");
         return Enumerable.Range(1, 5).Select(index => new WeatherForecast
         {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = "Good"
         })
         .ToArray();
      }

      [HttpGet("/today", Name = "GetTodaysForecast")]
      public WeatherForecast GetTodaysForecast([FromQuery] string zipCode)
      {
         _logger.LogInformation("Received request to get weather forecast.");
         return _weatherService.GetWeatherForecast(zipCode);
      }
   }
}