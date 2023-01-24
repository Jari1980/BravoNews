using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Queues;
namespace AddThingsToQueue
{
    public static class WeeklyAddThingsToQueue
    {
        [FunctionName("WeeklyAddThingsToQueue")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.setting.json", true)
                .AddEnvironmentVariables()
                .Build();

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            string connectionString = configuration["AzureWebJobsStorage"];
            string queueString = configuration["AzureQueueNameWeekly"];

            QueueClient queueClient = new QueueClient(connectionString, queueString, new
                                    QueueClientOptions()
            { MessageEncoding = QueueMessageEncoding.Base64 });

            queueClient.CreateIfNotExists();

            try
            {
                queueClient.SendMessage(requestBody);
                log.LogInformation("item sent to queue");
            }
            catch (Exception)
            {
                log.LogInformation("Oops, something went wrong");
            }

            //string subscribername = req.Query["name"];

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //subscribername = subscribername ?? data?.subscribername;

            //string responseMessage = string.IsNullOrEmpty(subscribername)
            //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //    : $"Hello, {subscribername}. This HTTP triggered function executed successfully.";

            //log.LogInformation(responseMessage);
            return new OkObjectResult("");
        }
    }
}
