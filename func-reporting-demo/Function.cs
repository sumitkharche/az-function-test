using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
namespace func_reporting_demo
{
    public class Function
    {
        private readonly ILogger<Function> _logger;

        public Function(ILogger<Function> logger)
        {
            _logger = logger;
        }

        [Function("test")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody =  new StreamReader(req.Body).ReadToEndAsync().Result ?? "";
            dynamic data = JsonConvert.DeserializeObject<Dictionary<string,object>>(requestBody) ?? [];
            _logger.LogInformation($"-----------reportId: {(string)data["reportId"]}");
            //_logger.LogInformation($"reportId: {data?.reportId}");
            bool isMockAPIEnabled = Convert.ToBoolean(Environment.GetEnvironmentVariable("EnableMockAPI"));
            return new OkObjectResult($"Welcome to Azure Functions! isMockAPIEnabled:{isMockAPIEnabled}");
        }
    }
}
