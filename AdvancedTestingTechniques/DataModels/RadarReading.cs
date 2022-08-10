namespace AdvancedTestingTechniques.DataModels
{
   public class RadarReading
   {
      public int Longitude { get; set; }
      public int Latitude { get; set; }

      /// <summary>
      /// Precipitation likelihood in percentage
      /// </summary>
      public int PrecipitationChance { get; set; }
   }
}
