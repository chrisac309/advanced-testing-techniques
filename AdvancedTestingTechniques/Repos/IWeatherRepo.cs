namespace AdvancedTestingTechniques.Repos
{
   public interface IWeatherRepo
   {
      /// <summary>
      /// Tries to retrieve an existing forecast if one was already requested
      /// </summary>
      bool TryGetWeatherForecast(int latitude, int longitude, DateTime date, out WeatherForecast? existingForecast);
      void StoreWeatherForecast(WeatherForecast forecast);
   }

   public class WeatherRepo : IWeatherRepo
   {
      private readonly ILogger _logger;

      public WeatherRepo(
         ILogger<WeatherRepo> logger)
      {
         _logger = logger;
      }

      public void StoreWeatherForecast(WeatherForecast forecast)
      {
         _logger.LogInformation("Storing weather forecast");
      }

      public bool TryGetWeatherForecast(int latitude, int longitude, DateTime date, out WeatherForecast? existingForecast)
      {
         existingForecast = null;
         if (date.Date == DateTime.Now.AddDays(-1).Date)
         {
            // We always know yesterday's weather
            existingForecast = new WeatherForecast
            {
               Date = date,
               Humidity = 30,
               PrecipitationChance = 30,
               Summary = "Good",
               TemperatureC = 15
            };
            return true;
         }
         return false;
      }
   }
}
