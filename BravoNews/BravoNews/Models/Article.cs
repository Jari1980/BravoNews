using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BravoNews.Models
{
    public class Article

    {


        [Key]
        public int Id  { get; set; }
        public DateTime DateStamp{ get; set; }
        public DateTime DateStampEditorsChoice { get; set; }
        public string LinkText { get; set; }
        public string Headline { get; set; }
        public string ContentSummary { get; set; }
        public string Content { get; set; }
        public int Views { get; set; }
        public int Likes { get; set; }
        public Uri ImageLink { get; set; }
       // public virtual ICollection<Uri> ListTest { get; set; }
        public string FileName { get; set; }
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public bool Archived { get; set; }
    }

}

