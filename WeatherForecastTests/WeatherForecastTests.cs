using AdvancedTestingTechniques;
using AdvancedTestingTechniques.DataModels;
using AdvancedTestingTechniques.Repos;
using AdvancedTestingTechniques.Services;
using AutoFixture;
using AutoFixture.Idioms;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using PerceptPlusTestHarness;
using System;
using Xunit;

namespace WeatherForecastTests
{
   /// <summary>
   /// Advanced Unit Testing session
   /// <list>
   /// AutoFixture
   /// Frozen attribute
   /// Mock
   /// sut
   /// AutoData
   /// InlineAutoData
   /// AppAutoData
   /// Fluent Assertions
   /// Code Coverage Analysis
   /// Add Watch   
   /// Live unit testing
   /// TDD -> Tomorrow's Forecast
   /// PR Process, Ratcheting
   /// </list>
   /// </summary>
   public class WeatherForecastTests
   {
      [Theory, AppAutoData]
      public void GetWeatherForecast_ReturnsExistingForecast_WhenForecastExistsInRepo(
         string zipCode,
         Location location,
         WeatherForecast expectedForecast,
         [Frozen] Mock<ILocationService> mockLocationService,
         [Frozen] Mock<IWeatherRepo> mockWeatherRepo,
         WeatherReportService sut)
      {
         // Arrange
         // Uncomment below to demonstrate what happens without a setup
         mockLocationService.Setup(s => s
            .GetLocation(zipCode))
            .Returns(location)
            .Verifiable();
         mockWeatherRepo.Setup(repo => repo
            .TryGetWeatherForecast(
            location.Latitude,
            location.Longitude,
            It.IsAny<DateTime>(), // Less strict
            //It.Is<DateTime>(d => d == DateTime.Now.Date) // More strict
            out expectedForecast
            ))
            .Returns(true)
            .Verifiable();

         // Act
         var forecast = sut.GetWeatherForecast(zipCode);

         // Assert
         forecast.Should().Be(expectedForecast, "We found an existing forecast.");
         mockLocationService.Verify();
         mockWeatherRepo.Verify();
      }

      // Comment out this test to demonstrate code coverage
      [Theory, AppAutoData]
      public void GetWeatherForecast_ReturnsNewForecast_WhenForecastDoesNotExistInRepo(
         // The Frozen attribute tells AutoFixture to use this instance in constructors
         // Mock is a "fake" version of an object
         string zipCode,
         int humidity,
         int temperature,
         Location location,
         RadarReading radarReading,
         [Frozen] Mock<ILocationService> mockLocationService,
         [Frozen] Mock<IHumidityService> mockHumidityService,
         [Frozen] Mock<IRadarService> mockRadarService,
         [Frozen] Mock<ITemperatureService> mockTemperatureService,
         [Frozen] Mock<IWeatherRepo> mockWeatherRepo,
         WeatherReportService sut)
      {
         // Arrange
         mockLocationService.Setup(s => s
            .GetLocation(zipCode))
            .Returns(location)
            .Verifiable();

         var anyForecast = new WeatherForecast();
         mockWeatherRepo.Setup(repo => repo
            .TryGetWeatherForecast(
               It.IsAny<int>(), 
               It.IsAny<int>(),
               It.IsAny<DateTime>(),
               out anyForecast))
            .Returns(false);

         mockHumidityService.Setup(h => h.GetHumidity(location.Latitude, location.Longitude))
            .Returns(humidity)
            .Verifiable();

         mockRadarService.Setup(r => r
            .GetRadarReading(location.Latitude, location.Longitude))
            .Returns(radarReading)
            .Verifiable();

         mockTemperatureService.Setup(t => t
            .GetTemperature(location.Latitude, location.Longitude))
            .Returns(temperature)
            .Verifiable();

         // Act
         var forecast = sut.GetWeatherForecast(zipCode);

         // Assert
         forecast.Date.Should().Be(DateTime.Now.Date);
         forecast.Summary.Should().NotBeEmpty("It is assigned randomly.");
         forecast.Humidity.Should().Be(humidity);
         forecast.PrecipitationChance.Should().Be(radarReading.PrecipitationChance);
         forecast.TemperatureC.Should().Be(temperature);

         // Verify setup mocks
         mockLocationService     .Verify();
         mockWeatherRepo         .Verify();
         mockHumidityService     .Verify();
         mockTemperatureService  .Verify();

         // Verify mocks without a setup (if any)
         mockWeatherRepo.Verify(repo => repo
            .StoreWeatherForecast(It.Is<WeatherForecast>(f =>
               f.Date == DateTime.Now.Date &&
               f.Humidity == humidity &&
               f.PrecipitationChance == radarReading.PrecipitationChance &&
               f.TemperatureC == temperature)));
      }

      [Theory, AppAutoData]
      public void Constructor_ShouldHaveGuards(
         IFixture fixture)
      {
         // AutoFixture has a really cool, and often overseen extension called Idioms, which are unit tests that use common templates
         var assertion = new GuardClauseAssertion(fixture);
         assertion.Verify(typeof(WeatherReportService).GetConstructors());
      }
   }
}