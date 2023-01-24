using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SendEmail.Models;
using SendGrid.Helpers.Mail;

namespace SendEmail
{
    public class SendEmailSubscriptionFiveDays
    {
        [FunctionName("SendEmailSubscriptionFiveDays")]
        [return: SendGrid(ApiKey = "SendGridKey", From = "lexiconbroccoli@gmail.com")]
        public SendGridMessage Run([QueueTrigger("fivedaystoexpirequeue", Connection = "AzureWebJobsStorage")]Email item, ILogger log)
        {
            SendGridMessage message = new SendGridMessage()
            {
                Subject = $"Your Bravo News subscription is about to expire"
            };
            
            message.AddContent("text/html",
            $"<a href='https://imgbb.com/'><img src='https://i.ibb.co/3vsJkXq/bravo-nlogo-1.png' alt='bravo-nlogo-1' border='0'></a>" +
            $"<h2>Dear {item.SubcriberName},</h2>" +
            $" <p>Your subscription at Bravo News is about to expire {DateTime.Now.AddDays(5).ToShortDateString()}. " +
            $"We hope you’ve enjoyed the benefits of being subscribed and would like to renew your subscription.</p>" +
            $" <footer>  ©{DateTime.Now.Year} - BravoNews - </footer");
            message.AddTo(item.SubscriberEmail, item.SubcriberName);
            return message;
        }
    }
}
