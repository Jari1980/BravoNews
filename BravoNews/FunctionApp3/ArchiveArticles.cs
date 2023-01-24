using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using TimerNewsApp.Services;

namespace TimerNewsApp
{
    public class ArchiveArticles
    {
        private readonly ILogger _logger;
        private readonly IArchiveNewsService _archiveNewsService;

        public ArchiveArticles(ILoggerFactory loggerFactory, IArchiveNewsService archiveNewsService)
        {
            _logger = loggerFactory.CreateLogger<ArchiveArticles>();
            _archiveNewsService = archiveNewsService;
        }

        [Function("ArchiveArticles")]
        public void Run([TimerTrigger("0 0 0 * * *")] MyInfo3 myTimer) // every day at 00:00:00
        {
            _archiveNewsService.ArchiveArticles();
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
        }
    }

    public class MyInfo3
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus3
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
