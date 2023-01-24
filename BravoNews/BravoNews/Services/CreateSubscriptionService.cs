using BravoNews.Models;
    namespace BravoNews.Services
{
    public class CreateSubscriptionService : ICreateSubscriptionService
    {
        public Email Subscription()
        {

            Email newEmail = new()
            {
                SubscriberEmail = "sgmohite02@gmail.com",
                SubcriberName = "Sujata",
                SubscriptionTypeName = "Premium"
            };
            return newEmail;
        }


    } 
    
}
