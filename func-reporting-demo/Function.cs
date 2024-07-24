using Azure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Users.Item.SendMail;
using Microsoft.Identity.Client;
using Microsoft.Kiota.Abstractions;
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
            //string requestBody =  new StreamReader(req.Body).ReadToEndAsync().Result ?? "";
            //dynamic data = JsonConvert.DeserializeObject<Dictionary<string,object>>(requestBody) ?? [];
            //_logger.LogInformation($"-----------reportId: {(string)data["reportId"]}");
            ////_logger.LogInformation($"reportId: {data?.reportId}");
            //bool isMockAPIEnabled = Convert.ToBoolean(Environment.GetEnvironmentVariable("EnableMockAPI"));


        //    string fromEmail = "codewithsumit@outlook.com";
        //    try
        //    {
        //        var graphClient = GetGraphClient();

        //        var message = new Message
        //        {
        //            Subject = "subject",
        //            Body = new ItemBody
        //            {
        //                ContentType = BodyType.Html,
        //                Content = "body"
        //            },
        //            ToRecipients = new List<Recipient>
        //        {
        //            new Recipient
        //            {
        //                EmailAddress = new EmailAddress
        //                {
        //                    Address = "codewithsumit@outlook.com"
        //                }
        //            }
        //        }
        //        };
        //        var requestBody = new SendMailPostRequestBody
        //        {
        //            Message = new Message
        //            {
        //                Subject = "Meet for lunch?",
        //                Body = new ItemBody
        //                {
        //                    ContentType = BodyType.Text,
        //                    Content = "The new cafeteria is open.",
        //                },
        //                ToRecipients = new List<Recipient>
        //{
        //    new Recipient
        //    {
        //        EmailAddress = new EmailAddress
        //        {
        //            Address = "codewithsumit@outlook.com",
        //        },
        //    },
        //},
                  
        //            },
        //            SaveToSentItems = false,
        //        };

        //        await graphClient.Users?[fromEmail]?.SendMail.PostAsync(requestBody);


        //        return new OkObjectResult("Email sent successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Error sending email: {ex.Message}");
        //        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        //    }

            return new OkObjectResult($"Welcome to Azure Functions!");



        }
    }
}
