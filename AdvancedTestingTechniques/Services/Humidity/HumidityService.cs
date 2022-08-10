using AdvancedTestingTechniques.Services.Humidity;

namespace AdvancedTestingTechniques.Services
{
   public interface IHumidityService
   {
      /// <summary>
      /// Gets the humidity level at the specified location.
      /// </summary>
      int GetHumidity(int latitude, int longitude);
   }

   public class HumidityService : IHumidityService
   {
      private readonly IHumidityReader _reader;
      private readonly ILogger _logger;

      public HumidityService(
         IHumidityReader humidityReader,
         ILogger<HumidityService> logger)
      {
         _reader = humidityReader ?? throw new ArgumentNullException(nameof(humidityReader));
         _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      }

      public int GetHumidity(int latitude, int longitude)
      {
         _logger.LogInformation("Getting humidity for Lat: {latitude} and Long: {longitude}", latitude, longitude);
         return _reader.ReadHumidity(latitude, longitude);
      }
   }
}
