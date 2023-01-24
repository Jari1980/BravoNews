using System.ComponentModel.DataAnnotations;

namespace BravoNews.Models
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }
        public int SubscriptionTypeId { get; set; }
        public virtual SubscriptionType SubscriptionType { get; set; }
        public double Price { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
        public virtual User User { get; set; }
        public bool PaymentComplete { get; set; }
        public bool Active { get; set; }
    }
}
