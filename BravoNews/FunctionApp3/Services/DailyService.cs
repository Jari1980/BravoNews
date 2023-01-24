using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TimerNewsApp.Data;
using TimerNewsApp.Model.FuncModels;


namespace TimerNewsApp.Services
{
    public class DailyService : IDailyService
    {
        private readonly FuncDbContext _db;
        private readonly ILogger _logger;
        public DailyService(FuncDbContext db, ILoggerFactory logger)
        {
            _db = db;
            _logger = logger.CreateLogger<DailyService>();
        }
        public List<SubscriptionDataFM> GetSubscriptionsToExpire()
        {
            var dateInFiveDays = DateTime.Now.AddDays(5).Date;
            var expiringSubscriptionsData =
             _db.Users.Include(u => u.Subscriptions).Where(u => 
             u.Subscriptions.Any(s => s.Active && s.Expires.Date < dateInFiveDays))
             .Select(e => new SubscriptionDataFM()
            {
                SubscriberName = e.UserName,
                SubscriberEmail = e.Email,
                SubscriptionTypeName = e.Subscriptions.FirstOrDefault().SubscriptionType.TypeName
            });
            return expiringSubscriptionsData.ToList();
        }

        public void SetSubscriptionExpired()
        {
            var result = _db.Subscriptions.Where(s => s.Expires < DateTime.Now);
           foreach(var item in result)
            {
                item.Active = false;
                _db.Update(item);
                _logger.LogInformation("Archived" + item.Id + "was archived");
            }
           _db.SaveChanges();
        }

        public void SetArticleArchived()
        {
            var thirtyDaysAgo = DateTime.Now.AddDays(-30);
            var result = _db.Articles.Where(a => a.DateStamp < thirtyDaysAgo).ToList();
            foreach(var item in result)
            {
                item.Archived = true;
                _db.Update(item);
            }
            _db.SaveChanges();  
        }
    }


}
