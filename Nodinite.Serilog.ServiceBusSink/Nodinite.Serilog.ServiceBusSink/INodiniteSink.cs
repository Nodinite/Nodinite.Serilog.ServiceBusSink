using Nodinite.Serilog.Models;

namespace Nodinite.Serilog.ApiSink
{
    interface INodiniteSink
    {
        void LogMessage(NodiniteLogEvent logEvent);
    }
}
