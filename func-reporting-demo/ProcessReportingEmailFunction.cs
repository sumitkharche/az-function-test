using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text;

namespace func_reporting_demo
{
    public class ProcessReportingEmailFunction
    {
        private readonly ILogger<ProcessReportingEmailFunction> _logger;

        public ProcessReportingEmailFunction(ILogger<ProcessReportingEmailFunction> logger)
        {
            _logger = logger;
        }

        [Function(nameof(ProcessReportingEmailFunction))]
        public async Task Run(
            [ServiceBusTrigger("reporting", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation($"{nameof(ProcessReportingEmailFunction)} requested.");
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation($"{nameof(ProcessReportingEmailFunction)} - Request Body captured: {message.Body}");
            var reportingRequest = message.Body.ToString();
            await SendReportingEmail(reportingRequest);

            // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }

        private async Task SendReportingEmail(string reportingRequest)
        {
            bool isMockAPIEnabled = Convert.ToBoolean(Environment.GetEnvironmentVariable("EnableMockAPI"));
            _logger.LogInformation($"{nameof(ProcessReportingEmailFunction)}- IsMockAPIEnabled {isMockAPIEnabled}");
            if (isMockAPIEnabled)
            {
                try
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        var mockApiUrl = Environment.GetEnvironmentVariable("MockApiUrl");
                        _logger.LogInformation($"{nameof(ProcessReportingEmailFunction)}- MockAPIURL: {mockApiUrl}");
                        var requestContent = new StringContent(reportingRequest, Encoding.UTF8, "application/json");
                        httpClient.DefaultRequestHeaders.Add("ocp-apim-subscription-key", "dc4e1a96d41d4301932347c7607b54f0");
                        var response = await httpClient.PostAsync(mockApiUrl, requestContent);
                        _logger.LogInformation("Resp status: {0}", response.StatusCode);

                        if (response.IsSuccessStatusCode)
                        {
                            var responseMessage = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                            _logger.LogInformation("Mock API response: {0}", responseMessage);
                        }
                        else
                        {
                            _logger.LogInformation("Failed to search matching domain contact in CRM. Reason");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error while processing mock api message: {ex.Message}");
                    throw;
                }
            }
            else
            {
                _logger.LogInformation("---Calling PowerBIAPI {0}", isMockAPIEnabled);
            }
        }
    }
}
