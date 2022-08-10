namespace AdvancedTestingTechniques
{
   public class WeatherForecast
   {
      public DateTime Date { get; set; }

      public int TemperatureC { get; set; }

      public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

      public int PrecipitationChance { get; set;}
      public int Humidity {get;set; }

      public string? Summary { get; set; }
   }
}