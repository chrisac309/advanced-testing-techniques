namespace AdvancedTestingTechniques.Services
{
   public interface ITemperatureService
   {
      /// <summary>
      /// Returns a temperature in Celsius
      /// </summary>
      int GetTemperature(int latitude, int longitude);
   }

   public class TemperatureService : ITemperatureService
   {
      private readonly ILogger _logger;

      public TemperatureService (
         ILogger<TemperatureService> logger
         )
      {
         _logger = logger ?? throw new ArgumentNullException (nameof (logger));
      }

      /// <summary>
      /// Always return 15
      /// </summary>
      public int GetTemperature(int latitude, int longitude)
      {
         _logger.LogInformation("Getting temperature for Lat: {latitude} and Long: {longitude}", latitude, longitude);
         return 15;
      }
   }
}
