using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SendEmailSocketLabs.Models;

namespace SendEmailSocketLabs
{
    public class SendEmail
    {
        [FunctionName("SendEmail")]
        public void Run([QueueTrigger("mailqueue", Connection = "AzureWebJobsStorage")]Email item, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {item.SubcriberName}");

            const string smtpHost = "smtp.socketlabs.com";
            const int smtpPort = 2525; // standard is port 25 but that is blocked by many ISP's
            const string smtpUserName = "server46730";  //"YOUR-SMTP-USER-NAME";
            const string smtpPassword = "Gy34Zsz2S9TjRr76";

            var creds = new NetworkCredential(smtpUserName, smtpPassword);
            var auth = creds.GetCredential(smtpHost, smtpPort, "Basic");

            using (var msg = new MailMessage())
            using (var smtp = new SmtpClient())
            {
                // ** can be set in config **
                // you can skip this set by setting your credentials in the web.config or app.config
                // http://msdn.microsoft.com/en-us/library/w355a94k.aspx
                smtp.Host = smtpHost;
                smtp.Port = smtpPort;
                smtp.Credentials = auth;

                //Add SocketLabs MessageID and MailingID [ https://support.socketlabs.com/kb/48 ]
                msg.Headers.Add("X-xsMessageId", "MyCampaign");
                msg.Headers.Add("X-xsMailingId", "12345");

                if (item.SubscriptionTypeName == "Registered")
                {

                    msg.From = new MailAddress("bravonews@dreammaker-it.se");
                    msg.To.Add(item.SubscriberEmail);
                    msg.Subject = "Thank you for subscribing";
                    msg.Body = $"<h1>Hello {item.SubcriberName} </h1><p>Thank you for registering!</p>";
                    msg.IsBodyHtml = true;

                    smtp.Send(msg);
                }
                else
                {
                    msg.From = new MailAddress("bravonews@dreammaker-it.se");
                    msg.To.Add(item.SubscriberEmail);
                    msg.Subject = "Using the SMTP client with SocketLabs.";
                    msg.Body = $"<h1>Hello {item.SubcriberName}</h1><p>You are now registered as a premium user, enjoy the site!</p>";
                    msg.IsBodyHtml = true;

                    smtp.Send(msg);
                }

            }

            Console.WriteLine("Email Sent");
            Console.Read();
        }

    
    }
}
