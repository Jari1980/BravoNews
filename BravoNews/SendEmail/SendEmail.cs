// The 'From' and 'To' fields are automatically populated with the values specified by the binding settings.
//
// You can also optionally configure the default From/To addresses globally via host.config, e.g.:
//
// {
//   "sendGrid": {
//      "to": "user@host.com",
//      "from": "Azure Functions <samples@functions.com>"
//   }
// }
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Logging;
using SendEmail.Models;

namespace SendEmail
{
    public class SendEmail
    {
        //[FunctionName("SendWeeklyEmail")]
        //[return: SendGrid(ApiKey = "SendGridKey", From = "lexiconbroccoli@gmail.com")]
        //public SendGridMessage Run([QueueTrigger("mailqueue", Connection = "AzureWebJobsStorage")] Email item, Uri Blob)
        //{
        //    SendGridMessage message = new SendGridMessage()
        //    {
        //        Subject = $"Weekly Supermail (#{item.SubcriberName})!"
        //    };
        //    message.AddContent("text/html", $"<h2>Dear {item.SubcriberName},</h2> <p> {Blob}</p>");
        //    message.AddTo(item.SubscriberEmail, item.SubcriberName);
        //    return message;
        //}


        [FunctionName("SendEmail")]
        [return: SendGrid(ApiKey = "SendGridKey", From = "lexiconbroccoli@gmail.com")]
        public SendGridMessage Run([QueueTrigger("mailqueue", Connection = "AzureWebJobsStorage")] Email item, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function subscription for: {item.SubcriberName}");

            if(item.SubscriptionTypeName == "ContactForm")
            {
                SendGridMessage message = new SendGridMessage()
                {
                    Subject = $"Bravo News: Someone has something to say"
                };
                message.AddContent("text/html",
                    $"<a href='https://imgbb.com/'><img src='https://i.ibb.co/3vsJkXq/bravo-nlogo-1.png' alt='bravo-nlogo-1' border='0'></a>" +
                    $"<h2> Dear BroccoliMaster,</h2>" +
                    $" <p> {item.SubcriberName}</p>" +
                    $" <p> Message was send {item.Expires}.</p>" +
                    $" <footer> ©{DateTime.Now.Year} - BravoNews - </footer>");
                message.AddTo("lexiconbroccoli@gmail.com", "BroccoliMasters");
                return message;
            }

            else if (item.SubscriptionTypeName == "Registered")
            {
                SendGridMessage message = new SendGridMessage()
                {
                    Subject = $"Bravo News: New Member Registration"
                };
                message.AddContent("text/html",
                    $"<a href='https://imgbb.com/'><img src='https://i.ibb.co/3vsJkXq/bravo-nlogo-1.png' alt='bravo-nlogo-1' border='0'></a>" +
                    $"<h2> Dear {item.SubcriberName},</h2>" +
                    $"<h3> Thank you for regestering at Bravo News.</h3>" +
                    $" <p> You are now a member and can access our articles, you can at any point upgrade your account to a premium subscription in order to access all articles and get some wisdom from Chuck Norris</p>"  +
                    $" <footer> ©{DateTime.Now.Year} - BravoNews - </footer>");
                message.AddTo(item.SubscriberEmail, item.SubcriberName);
                return message;
            }
            else if(item.SubscriptionTypeId == 1)
            {
                SendGridMessage message = new SendGridMessage()
                {
                    Subject = $"Bravo News: Subscription Confirmation"
                };
                message.AddContent("text/html", 
                    $"<a href='https://imgbb.com/'><img src='https://i.ibb.co/3vsJkXq/bravo-nlogo-1.png' alt='bravo-nlogo-1' border='0'></a>" +
                    $"<h2> Dear {item.SubcriberName},<h2> " +
                    $"<h3> Thank you for subscribing to our site!</h3>" +
                     $"<p>Your one month subscription for ${item.Price} will expire {item.Expires.ToShortDateString()} </p>" +
                    $"<p> We hope you will enjoy the premium features of our site. </p> " +
                    $" <footer> ©{DateTime.Now.Year} - BravoNews - </footer>");
                message.AddTo(item.SubscriberEmail, item.SubcriberName);
                return message;
            }
            else
            {
                SendGridMessage message = new SendGridMessage()
                {
                    Subject = $"Bravo News: Subscription Confirmation"
                };
                message.AddContent("text/html", 
                    $"<a href='https://imgbb.com/'><img src='https://i.ibb.co/3vsJkXq/bravo-nlogo-1.png' alt='bravo-nlogo-1' border='0'></a>" +
                    $"<h2> Dear {item.SubcriberName},<h2> " +
                    $"<h3> Thank you for subscribing to our site!</h3>" +
                    $"<p> Your one year subscription for ${item.Price} will expire {item.Expires.ToShortDateString()} </p>" +
                    $"<p> We hope you will enjoy the premium features of our site. </p>" +
                    $" <footer> ©{DateTime.Now.Year} - BravoNews - </footer>");
                message.AddTo(item.SubscriberEmail, item.SubcriberName);
                return message;
            }
            
        }
    }

}
