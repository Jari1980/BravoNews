using System.ComponentModel.DataAnnotations;

namespace BravoNews.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime Date { get; set; }
        public bool Liked { get; set; }
    }
}
