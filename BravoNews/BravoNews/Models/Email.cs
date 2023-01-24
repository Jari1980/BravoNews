namespace BravoNews.Models
{
    public class Email
    {
        public int Id { get; set; }
        public string SubscriberEmail { get; set; }
        public string SubcriberName { get; set; }
        public string SubscriptionTypeName { get; set; }
        public int SubscriptionTypeId { get; set; }
        public double Price { get; set; }
        public DateTime Expires { get; set; }
    }
}
