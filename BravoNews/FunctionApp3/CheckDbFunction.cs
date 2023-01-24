using System;
using Azure.Storage.Queues;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TimerNewsApp.Services;


namespace TimerNewsApp
{
    public class CheckDbFunction
    {
        private readonly ILogger _logger;
        private readonly IDailyService _dailyService;
        private readonly IConfiguration _configuration;
        private readonly IWeeklyService _weeklyService;
        public CheckDbFunction(ILoggerFactory loggerFactory, IDailyService dailyService,
                                  IConfiguration confifuration, IWeeklyService weeklyService)
        {
            _logger = loggerFactory.CreateLogger<CheckDbFunction>();
            _dailyService = dailyService;
            _configuration = confifuration;
            _weeklyService = weeklyService;
        }

        [Function("Function1")]
        public void Run([TimerTrigger("0 59 23 * * *")] MyInfo myTimer) // Every minute 0 * * * * *, Every day 0 0 0 * * *, default 0 59 23 * * *
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
             var result = _dailyService.GetSubscriptionsToExpire();
           
            QueueClient queueClient = new QueueClient(_configuration["AzureWebJobsStorage"], _configuration["AzureQueueName"], new QueueClientOptions() { MessageEncoding = QueueMessageEncoding.Base64});
            queueClient.CreateIfNotExists();
            foreach(var item in result)
            {
                queueClient.SendMessage(JsonConvert.SerializeObject(item));
            }
            _dailyService.SetSubscriptionExpired();
           //_dailyService.SetArticleArchived();
        }
    //    [Function("Function2")]
    //    public void Run([TimerTrigger("0 * * * * *")] SendWeekly myTimer)
    //    {
    //        _weeklyService.SubscriberLetter();
    //    }
    }

    //public class SendWeekly
    //{

    //}
    public class MyInfo
    {
        public MyScheduleStatus? ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
