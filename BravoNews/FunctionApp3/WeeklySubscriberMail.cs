using System;
using Azure.Storage.Queues;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TimerNewsApp.Data;
using TimerNewsApp.Services;
using TimerNewsApp.Model;
//using Microsoft.Azure.WebJobs.Extensions.Http;
//using Microsoft.AspNetCore.Http;

namespace TimerNewsApp
{
    public class WeeklySubscriberMail
    {
        private readonly ILogger _logger;
        private readonly IDailyService _dailyService;
        private readonly IConfiguration _configuration;
        private readonly IWeeklyService _weeklyService;
        //private readonly HttpRequest req;

        public WeeklySubscriberMail(ILoggerFactory loggerFactory, IDailyService dailyService, IConfiguration configuration, IWeeklyService weeklyService)
        {
            _logger = loggerFactory.CreateLogger<WeeklySubscriberMail>();
            _dailyService = dailyService;
            _configuration = configuration;
            _weeklyService = weeklyService;
        }

        [Function("WeeklySubscriberMail")]
        public void Run([TimerTrigger("0 * * * * *")] MyInfo2 myTimer) //Every minute 0 * * * * *, Once every sunday 0 0 0 * * 0
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            _weeklyService.SubscriberLetter();
            //Email email = new Email();
            List<Email> emailList = new List<Email>();
            emailList = _weeklyService.SubscriberLetter();



            //string requestBody = new StreamReader(req.Body).ReadToEndAsync();

            string connectionString = _configuration["AzureWebJobsStorage"];
            string queueString = _configuration["AzureQueueName2"];

            QueueClient queueClient = new QueueClient(connectionString, queueString, new
                                    QueueClientOptions()
            { MessageEncoding = QueueMessageEncoding.Base64 });

            queueClient.CreateIfNotExists();

            foreach (var item in emailList)
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

    public class MyInfo2
    {
        public MyScheduleStatus2 ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus2
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
