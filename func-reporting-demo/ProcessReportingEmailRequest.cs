using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace func_reporting_demo
{
    public class ProcessReportingEmailRequest
    {
        private readonly ILogger<ProcessReportingEmailRequest> _logger;

        public ProcessReportingEmailRequest(ILogger<ProcessReportingEmailRequest> logger)
        {
            _logger = logger;
        }

        [Function(nameof(ProcessReportingEmailRequest))]
        public async Task Run(
            [ServiceBusTrigger("reporting", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
