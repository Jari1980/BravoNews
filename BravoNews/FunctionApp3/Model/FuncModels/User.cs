using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TimerNewsApp.Model
{
    public class User : IdentityUser
    {
        public virtual ICollection<Subscription> Subscriptions { get; set; }
        public bool PersonalizedNewsletter { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
