using AdvancedTestingTechniques.DataModels;

namespace AdvancedTestingTechniques.Services
{
   public interface IRadarService
   {
      RadarReading GetRadarReading(int latitude, int longitude);
   }

   public class RadarService : IRadarService
   {
      private readonly ILogger _logger;

      public RadarService (
         ILogger<RadarService> logger
         )
      {
         _logger = logger ?? throw new ArgumentNullException (nameof (logger));
      }
      public RadarReading GetRadarReading(int latitude, int longitude)
      {
         _logger.LogInformation("Getting radar reading for Lat: {latitude} and Long: {longitude}", latitude, longitude);
         return new RadarReading
         {
            Latitude = latitude,
            Longitude = longitude,
            PrecipitationChance = 20
         };
      }
   }
}
