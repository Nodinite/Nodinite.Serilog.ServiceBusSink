using Nodinite.Serilog.Models;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using System;

namespace Nodinite.Serilog.ServiceBusSink
{
    public static class NodiniteServiceBusSinkExtensions
    {
        public static LoggerConfiguration NodiniteServiceBusSink(
                  this LoggerSinkConfiguration loggerConfiguration,
                  string ConnectionString, 
                  string QueueName,
                  NodiniteLogEventSettings Settings,
                  IFormatProvider formatProvider = null,
                  LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum)
        {
            if (loggerConfiguration == null)
                throw new ArgumentNullException("loggerConfiguration");

            return loggerConfiguration.Sink(new NodiniteServiceBusSink(ConnectionString, QueueName, Settings, formatProvider), restrictedToMinimumLevel);
        }
    }
}
