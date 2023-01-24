using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailSocketLabs.Models
{
    public class Email
    {
        public string SubscriberEmail { get; set; }
        public string SubcriberName { get; set; }
        public string SubscriptionTypeName { get; set; }
        public Uri Blob { get; set; }
    }
}
