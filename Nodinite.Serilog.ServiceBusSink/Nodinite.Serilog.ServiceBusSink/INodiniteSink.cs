using Nodinite.Serilog.Models;

namespace Nodinite.Serilog.ServiceBusSink
{
    interface INodiniteSink
    {
        void LogMessage(NodiniteLogEvent logEvent);
    }
}
