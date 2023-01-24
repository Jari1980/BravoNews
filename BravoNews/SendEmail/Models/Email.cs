using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendEmail.Models
{
    public class Email
    {
        public string SubscriberEmail { get; set; }
        public string SubcriberName { get; set; }
        public string SubscriptionTypeName { get; set; }
        public Uri Blob { get; set; }
        public Uri Blob2 { get; set; }
        public Uri Blob3 { get; set; }
        public string ContentSummary { get; set; }
        public string ContentSummary2 { get; set; }
        public string ContentSummary3 { get; set; }
        public string Headline { get; set; }
        public string Headline2 { get; set; }
        public string Headline3 { get; set; }
        public int SubscriptionTypeId { get; set; }
        public double Price { get; set; }
        public DateTime Expires { get; set; }
    }
}
