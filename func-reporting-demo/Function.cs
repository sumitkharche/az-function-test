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

        //private static GraphServiceClient GetGraphClient()
        //{

        //    var scopes = new[] { "https://graph.microsoft.com/.default" };

        //    // Values from app registration
        //    var clientId = "01a2a325-0ef1-4012-afd9-e9a6a1623082";
        //    var tenantId = "49e76321-588d-4dfd-a4fa-6ae4204e10f7";
        //    var clientSecret = "PU98Q~UOT6JM4XOnnv-GjkMQY8tJw_ouEBNG1bHM";

        //    // using Azure.Identity;
        //    var options = new ClientSecretCredentialOptions
        //    {
        //        AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
        //    };

        //    var clientSecretCredential = new ClientSecretCredential(
        //        tenantId, clientId, clientSecret, options);

        //    var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

        //    return graphClient;
        //}
    }
}
