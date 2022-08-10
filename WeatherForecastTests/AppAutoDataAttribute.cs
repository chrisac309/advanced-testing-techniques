using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Microsoft.Extensions.Logging;
using System;
using Xunit;
using Xunit.Sdk;

namespace PerceptPlusTestHarness
{
   /// <summary>
	/// Works similar to the built in AutoDataAttribute but adds some
	/// app specific customizations to the fixture.
	/// </summary>
	[DataDiscoverer(
        typeName: "AutoFixture.Xunit2.NoPreDiscoveryDataDiscoverer",
        assemblyName: "AutoFixture.Xunit2")]
   [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class AppAutoDataAttribute : AutoDataAttribute
	{
		public static IFixture Create()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMoqCustomization
			{
				ConfigureMembers = true
			});
			fixture.Customize(new LoggerCustomization());
			return fixture;
		}

		public AppAutoDataAttribute() : base(Create)
		{
		}
	}

   public class LoggerCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			// Ensure that every instance of logging is our test logger
			fixture.Register<ILogger>(() => fixture.Create<TestLogger>());
		}
	}

   /// <summary>
   /// Works similarly to xUnit's InlineDataAttribute
   /// but allows you to add more parameters to the test method than what is defined in the attribute.
   /// Any parameters that cannot be resolved by the inline values in the attribute are
   /// resolved using AutoFixture with our app specific customizations.
   /// </summary>
   /// <example>
   /// <code>
   /// [Theory]
   /// [InlineAppAutoData("")]
   /// [InlineAppAutData((object) null)]
   /// public void ShouldThrowArgumentException(string email, UserService sut)
   /// {
   ///		...
   /// }
   /// </code>
   /// </example>
   [DataDiscoverer(
        typeName: "AutoFixture.Xunit2.NoPreDiscoveryDataDiscoverer",
        assemblyName: "AutoFixture.Xunit2")]
   [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class InlineAppAutoDataAttribute : CompositeDataAttribute
	{
		public InlineAppAutoDataAttribute(params object[] values)
			: base(new InlineDataAttribute(values), new AppAutoDataAttribute())
		{
		}
	}
}
