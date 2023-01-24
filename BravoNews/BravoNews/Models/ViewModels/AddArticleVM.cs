using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BravoNews.Models.ViewModels
{
    public class AddArticleVM
    {
        public AddArticleVM()
        {
            Categories = new List<SelectListItem>();
        }
        [Required]
        public string Headline { get; set; }
        public string LinkText { get; set; }
        [StringLength(100),Required]
        public string ContentSummary { get; set; }
        [Required]
        public string Content { get; set; }

        public int CategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public string FileName { get; set; }
        public IFormFile File { get; set; }

    }
}
