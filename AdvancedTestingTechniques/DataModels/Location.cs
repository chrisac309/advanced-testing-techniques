namespace AdvancedTestingTechniques.DataModels
{
   public class Location
   {
      public int Latitude { get; set; }
      public int Longitude { get; set; }
      public string City { get; set; } = string.Empty;
      public string State { get; set; } = string.Empty;
   }
}
