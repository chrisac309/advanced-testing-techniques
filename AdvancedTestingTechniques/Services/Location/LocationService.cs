using AdvancedTestingTechniques.DataModels;

namespace AdvancedTestingTechniques.Services
{
   public interface ILocationService
   {
      Location GetLocation(string zipCode);
   }

   public class LocationService : ILocationService
   {
      private readonly ILogger _logger;

      public LocationService (
         ILogger<LocationService> logger
         )
      {
         _logger = logger ?? throw new ArgumentNullException (nameof (logger));
      }

      public Location GetLocation(string zipCode)
      {
         _logger.LogInformation("Getting location for zip code {zipcode}", zipCode);
         return new Location
         {
            Latitude = 40,
            Longitude = 83,
            City = "Columbus",
            State = "Ohio"
         };
      }
   }
}
