using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace PerceptPlusTestHarness
{
   public class TestLogger : ILogger
   {
      public IDisposable BeginScope<TState>( TState state )
      {
         return new MemoryStream();
      }

      public bool IsEnabled( LogLevel logLevel )
      {
         return false;
      }

      public void Log<TState>( LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter )
      {

      }
   }
}