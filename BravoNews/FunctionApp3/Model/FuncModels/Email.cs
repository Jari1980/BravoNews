using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerNewsApp.Model
{
    public class Email
    {
        public int Id { get; set; }
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

    }
}
