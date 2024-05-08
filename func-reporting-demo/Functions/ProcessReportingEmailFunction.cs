using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Net.Http;
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
                _logger.LogInformation("Calling Mock API");
                await ProcessMockAPI(reportingRequest);
            }
            else
            {

                _logger.LogInformation("Calling Power BI API");
                await SendReportingEmailAsync(reportingRequest);


            }
        }
        private async Task ProcessMockAPI(string reportingRequest)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    //throw new TimeoutException($"Recieved failure from API RequestBody:{reportingRequest}");
                    var mockApiUrl = Environment.GetEnvironmentVariable("MockApiUrl");
                    _logger.LogInformation($"{nameof(ProcessReportingEmailFunction)}- MockAPIURL: {mockApiUrl}");
                    var requestContent = new StringContent(reportingRequest, Encoding.UTF8, "application/json");
                    var mockApiSubscriptionKey = Environment.GetEnvironmentVariable("MockApiSubscriptionKey");
                    httpClient.DefaultRequestHeaders.Add("ocp-apim-subscription-key", mockApiSubscriptionKey);
                    var response = await httpClient.PostAsync(mockApiUrl, requestContent);
                    _logger.LogInformation($"Mock API Resp status: {response.StatusCode}  RequestBody: {reportingRequest}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseMessage = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        _logger.LogInformation($"Mock API response: {responseMessage}");
                    }
                    else
                    {
                        _logger.LogInformation($"Error occured while calling the Mock API StatusCode: {response.StatusCode} RequestBody: {reportingRequest}");
                  
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while processing mock api message: {ex.Message}");
                throw;
            }
        }

        private string GetAccessToken()
        {
            _logger.LogInformation("Getting access token for Power BI API");
            return "token";
        }
        private async Task SendReportingEmailAsync(string reportingRequest)
        {
            try
            {
                string token = GetAccessToken();
                using (HttpClient httpClient = new HttpClient())
                {
                    var powerBIApiUrl = Environment.GetEnvironmentVariable("PowerBIApiUrl");
                    var requestContent = new StringContent(reportingRequest, Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", token);
                    var response = await httpClient.PostAsync(powerBIApiUrl, requestContent);
                    _logger.LogInformation("PowerBI API Response status: {0}", response.StatusCode);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseMessage = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        _logger.LogInformation("Power BI API response: {0}", responseMessage);
                    }
                    else
                    {
                        _logger.LogInformation($"Error occured while calling the PowerBI API StatusCode: {response.StatusCode} RequestBody: {reportingRequest}");
                      
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured while processing PowerBI API Exception: {ex.Message} RequestBody: {reportingRequest}");
                throw;
            }
        }
    }
}
