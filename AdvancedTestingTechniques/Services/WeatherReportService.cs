using AdvancedTestingTechniques.Repos;

namespace AdvancedTestingTechniques.Services
{
   public interface IWeatherReportService
   {
      /// <summary>
      /// Given a Zip Code, returns a weather forecast for that location.
      /// </summary>
      /// <param name="forZipCode"></param>
      /// <returns></returns>
      WeatherForecast GetWeatherForecast(string forZipCode);
   }

   public class WeatherReportService : IWeatherReportService
   {
      private static readonly string[] Summaries = new[]
      {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
      };

      private readonly ILocationService _locationService;
      private readonly IHumidityService _humidityService;
      private readonly IRadarService _radarService;
      private readonly ITemperatureService _temperatureService;

      private readonly IWeatherRepo _weatherRepo;

      public WeatherReportService (
         ILocationService locationService,
         IHumidityService humidityService,
         IRadarService radarService,
         ITemperatureService temperatureService,
         IWeatherRepo weatherRepo
         )
      {
         _locationService = locationService ?? throw new ArgumentNullException(nameof(locationService));
         _humidityService = humidityService ?? throw new ArgumentNullException(nameof(humidityService));
         _radarService = radarService ?? throw new ArgumentNullException(nameof(radarService)); 
         _temperatureService = temperatureService ?? throw new ArgumentNullException(nameof(temperatureService));

         _weatherRepo = weatherRepo ?? throw new ArgumentNullException(nameof(weatherRepo)); 
      }


      public WeatherForecast GetWeatherForecast(string forZipCode)
      {
         var location = _locationService.GetLocation(forZipCode);
         var date = DateTime.Now.Date;
         if (_weatherRepo.TryGetWeatherForecast(location.Latitude, location.Longitude, date, out var existingForecast))
         {
            return existingForecast;
         }

         var humidity = _humidityService.GetHumidity(location.Latitude, location.Longitude);
         var precipitation = _radarService.GetRadarReading(location.Latitude, location.Longitude).PrecipitationChance;
         var temperature = _temperatureService.GetTemperature(location.Latitude, location.Longitude);

         var forecast = new WeatherForecast {
            Date = DateTime.Now.Date,
            Summary = Summaries[Random.Shared.Next(Summaries.Length)],
            Humidity = humidity,
            PrecipitationChance = precipitation,
            TemperatureC = temperature
         };

         _weatherRepo.StoreWeatherForecast(forecast);

         return forecast;
      }
   }
}
