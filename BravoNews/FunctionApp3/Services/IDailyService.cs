using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimerNewsApp.Model.FuncModels;

namespace TimerNewsApp.Services
{
    public interface IDailyService
    {
        List<SubscriptionDataFM> GetSubscriptionsToExpire();
        void SetSubscriptionExpired();
        void SetArticleArchived();
    }
}
