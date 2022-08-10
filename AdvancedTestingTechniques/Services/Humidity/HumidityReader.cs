namespace AdvancedTestingTechniques.Services.Humidity
{
   public interface IHumidityReader
   {
      /// <summary>
      /// Gets the humidity reading as a percentage.
      /// </summary>
      int ReadHumidity(int latitude, int longitude);
   }

   public class HumidityReader : IHumidityReader
   {
      private readonly ILogger _logger;

      public HumidityReader (
         ILogger<HumidityReader> logger)
      {
         _logger = logger ?? throw new ArgumentNullException (nameof (logger));
      }

      /// <summary>
      /// Always returns 40.
      /// </summary>
      public int ReadHumidity(int latitude, int longitude)
      {
         _logger.LogInformation("Reading humidity for Lat: {latitude} and Long: {longitude}", latitude, longitude);
         return 40;
      }
   }
}
