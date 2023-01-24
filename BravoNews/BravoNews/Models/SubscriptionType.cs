using System.ComponentModel.DataAnnotations;

namespace BravoNews.Models
{
    public class SubscriptionType
    {
        [Key]
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
    }
}
