using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Storage.Queues;

namespace TimerTrigger
{
    public class TimerToQueue
    {
        [FunctionName("Function1")]
        public void Run([TimerTrigger("0 30 11 * * *", RunOnStartup = true)] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("local.settings.json", true, true)
                                    .AddEnvironmentVariables()
                                    .Build();

            var queueName = configuration["AzureQueueName"];
            QueueClient queueClient = new QueueClient(configuration["AzureWebJobsStorage"], queueName, new
                                        QueueClientOptions()
            { MessageEncoding = QueueMessageEncoding.Base64 });
            queueClient.CreateIfNotExists();

            Article newArticle = new()
            {
                Id = 1,
                Header = "Merry Christmas",
                Body = "Timer to queue"
            };
            var serializedArticle = JsonConvert.SerializeObject(newArticle);
            queueClient.SendMessage(serializedArticle);
            log.LogInformation("Item sent to queue");
        }
    }

    public class Article
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
    }
}
