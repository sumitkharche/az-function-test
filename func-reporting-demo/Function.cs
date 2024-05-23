using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

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
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            _logger.LogInformation($"Body: {req.Body}");
            bool isMockAPIEnabled = Convert.ToBoolean(Environment.GetEnvironmentVariable("EnableMockAPI"));
            return new OkObjectResult($"Welcome to Azure Functions! isMockAPIEnabled:{isMockAPIEnabled}");
        }
    }
}
