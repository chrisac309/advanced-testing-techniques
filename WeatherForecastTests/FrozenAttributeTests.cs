using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace WeatherForecastTests
{
   public class FrozenAttributeTests
   {
      [Theory, AutoData]
      public void FrozenAttribute_ShouldAssert(
         string stringOne,
         [Frozen] string stringTwo)
      {
         // The Frozen attribute says
         // "Every instance of a string that AutoFixture creates
         //  after this instance will be the same as this one."
         stringOne.Should().NotBe(stringTwo);
      }
   }
}