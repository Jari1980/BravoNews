using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Logging;
using SendEmail.Models;
using Newtonsoft.Json;
using SixLabors.ImageSharp.Memory;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace SendEmail
{
    public class SendWeeklyMail
    {
        [FunctionName("SendWeeklyMail")]
        [return: SendGrid(ApiKey = "SendGridKey", From = "lexiconbroccoli@gmail.com")]
        public SendGridMessage Run([QueueTrigger("weeklymailqueue", Connection = "AzureWebJobsStorage")] Email item)
        {
            SendGridMessage message = new SendGridMessage()
            {
                Subject = $"Bravo News Weekly SuperMail!"
            };
            //var test = JsonConvert.DeserializeObject<Email>(item);

            message.AddContent("text/html",

                $"<a href='https://imgbb.com/'><img src='https://i.ibb.co/3vsJkXq/bravo-nlogo-1.png' alt='bravo-nlogo-1' border='0'></a>" +
                $"<h2>Hi {item.SubcriberName},</h2> " +
                $"<h3>  Here's some of our newest articles. </h3> <br /> <br />" +
                $" <h3>{item.Headline}</h3> <img src='{item.Blob.AbsoluteUri}'/> <p>{item.ContentSummary}</p><br />" +
                $" <h3>{item.Headline2}</h3> <img src='{item.Blob2.AbsoluteUri}'/> <p>{item.ContentSummary2}</p> <br />" +
                $" <h3>{item.Headline3}</h3> <img src='{item.Blob3.AbsoluteUri}'/> <p>{item.ContentSummary3}</p>" +
                $" <footer>  ©{DateTime.Now.Year} - BravoNews - </footer");

      
            message.AddTo(item.SubscriberEmail, item.SubcriberName);
            return message;
        }
    }
}
