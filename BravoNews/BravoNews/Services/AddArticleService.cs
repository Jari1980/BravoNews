using BravoNews.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BravoNews.Services
{
    public class AddArticleService : IAddArticleService
    {
        private readonly IStorageService _storageService;
        private readonly IArticleService _articleService;

        public AddArticleService(IStorageService storageService, IArticleService articleService)
        {
            _storageService = storageService;
            _articleService = articleService;
        }

        public AddArticleVM AddArticle()
        {

            AddArticleVM newArticle = new();

            newArticle.Categories.Add(new SelectListItem { Text = "Sweden", Value = "1" });
            newArticle.Categories.Add(new SelectListItem { Text = "Weather", Value = "2" });
            newArticle.Categories.Add(new SelectListItem { Text = "Sports", Value = "3" });
            newArticle.Categories.Add(new SelectListItem { Text = "World", Value = "4" });
            newArticle.Categories.Add(new SelectListItem { Text = "Local", Value = "5" });
            newArticle.Categories.Add(new SelectListItem { Text = "Economy", Value = "6" });
            newArticle.Categories.Add(new SelectListItem { Text = "Health", Value = "7" });
            newArticle.Categories.Add(new SelectListItem { Text = "Advertisement", Value = "15" });
            return newArticle;
        }
        public void AddArticlePost(AddArticleVM newArticle)
        {
            string folderPath = "wwwroot/images/articles" + "/" + newArticle.CategoryId;
            string path = Path.Combine(Directory.GetCurrentDirectory(), folderPath);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileNameWithPath = Path.Combine(path, newArticle.FileName);
            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                newArticle.File.CopyTo(stream);
            }

            string pathFile = newArticle.CategoryId + "/" + newArticle.File.FileName;

            Uri blobUri = _storageService.UploadBlob(pathFile);

            _articleService.SaveArticle(newArticle, blobUri);
            return;
        }
    }
}
