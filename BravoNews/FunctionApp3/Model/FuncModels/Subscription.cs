using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimerNewsApp.Model.FuncModels;

namespace TimerNewsApp.Model
{
    public class Subscription
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime Expires { get; set; }
        public User User { get; set; }
        public virtual SubscriptionType SubscriptionType { get; set; }

    }
}
