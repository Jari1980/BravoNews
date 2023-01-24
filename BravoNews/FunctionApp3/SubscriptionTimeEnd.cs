using System;
using Azure.Storage.Queues;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TimerNewsApp.Services;

namespace TimerNewsApp
{
    public class SubscriptionTimeEnd
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IDailyService _dailyService;

        public SubscriptionTimeEnd(ILoggerFactory loggerFactory, IConfiguration configuration, IDailyService dailyService)
        {
            _logger = loggerFactory.CreateLogger<SubscriptionTimeEnd>();
            _configuration = configuration;
            _dailyService = dailyService;
        }

        [Function("SubscriptionTimeEnd")]
        public void Run([TimerTrigger("0 59 23 * * *")] MyInfo4 myTimer) // Every minute 0 * * * * *, Every day 0 59 23 * * *
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");

            var listSubscriptionTime = _dailyService.GetSubscriptionsToExpire();

            string connectionString = _configuration["AzureWebJobsStorage"];
            string queueString = _configuration["AzureQueueName3"];

            QueueClient queueClient = new QueueClient(connectionString, queueString, new
                                    QueueClientOptions()
            { MessageEncoding = QueueMessageEncoding.Base64 });

            queueClient.CreateIfNotExists();

            foreach (var item in listSubscriptionTime)
            {

                try
                {
                    queueClient.SendMessage(JsonConvert.SerializeObject(item));
                    _logger.LogInformation("item sent to queue");
                }
                catch (Exception)
                {
                    _logger.LogInformation("Oops, something went wrong");
                }
            }

        }
    }

    public class MyInfo4
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus4
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
