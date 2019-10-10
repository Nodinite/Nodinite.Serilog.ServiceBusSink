using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using Nodinite.Serilog.Models;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Nodinite.Serilog.ApiSink
{
    public class NodiniteServiceBusSink : ILogEventSink, INodiniteSink
    {
        private readonly IFormatProvider _formatProvider;
        private readonly string _connectionString;
        private readonly string _queueName;
        private readonly NodiniteLogEventSettings _settings;

        static IQueueClient queueClient;

        public NodiniteServiceBusSink(string connectionString, string queueName, NodiniteLogEventSettings settings, IFormatProvider formatProvider)
        {
            _connectionString = connectionString;
            _queueName = queueName;
            _settings = settings;
            _formatProvider = formatProvider;

            // validate settings
            if (!_settings.LogAgentValueId.HasValue)
                throw new ArgumentNullException("LogAgentValueId must not be null");
        }

        public void Emit(LogEvent logEvent)
        {
            var message = logEvent.RenderMessage(_formatProvider);

            var nEvent = new NodiniteLogEvent(message, logEvent, _settings);

            LogMessage(nEvent);
        }

        public void LogMessage(NodiniteLogEvent logEvent)
        {
            queueClient = new QueueClient(_connectionString, _queueName);

            var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(logEvent)));

            queueClient.SendAsync(message).Wait();
        }
    }
}
