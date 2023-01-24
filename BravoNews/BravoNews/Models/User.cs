using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BravoNews.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsPremium{ get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
        public bool PersonalizedNewsletter { get; set; }
    }
}
