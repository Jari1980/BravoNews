using BravoNews.Models;
using BravoNews.Models.ViewModels;
using BravoNews.Data;

namespace BravoNews.Services
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly ApplicationDbContext _db;
        private readonly IStorageService _storageService;

        public ArticleQuery(ApplicationDbContext db, IStorageService storageService)
        {
            _db = db;
            _storageService = storageService;
        }

        FirstPageVM obj = new FirstPageVM();

        public FirstPageVM IndexQueries() {
            string blobPath = "news-images-sm";
            obj.LatestNews = _db.Articles.Where(x => x.CategoryId != 15).Where(x => x.Archived == false).OrderByDescending(y => y.DateStamp).Take(5).ToList();
            foreach (var item in obj.LatestNews)
            {
                item.ImageLink = _storageService.GetBlob(item.FileName, blobPath, "/" + item.CategoryId);
                
            }

           obj.MostPopular = _db.Articles.Where(x => x.Archived == false).Where(x => x.DateStamp.AddDays(60) >= DateTime.Now).OrderByDescending(x => x.Views).Take(3).ToList();
            foreach (var item in obj.MostPopular)
            {
                item.ImageLink = _storageService.GetBlob(item.FileName, blobPath, "/" + item.CategoryId);

            }
            if (obj.MostPopular.Count <= 2)
            {
                obj.MostPopular = _db.Articles.Where(x => x.Archived == false).OrderByDescending(x => x.Views).Take(3).ToList(); //Added incase no uppdated arts
            }
            string blob = "news-images-xs";
            obj.EditorsChoice = _db.Articles.Where(x => x.Archived == false).OrderByDescending(x => x.DateStampEditorsChoice).Take(6).ToList();
            foreach (var item in obj.EditorsChoice)
            {
                item.ImageLink = _storageService.GetBlob(item.FileName, blob, "/" + item.CategoryId);

            }
            return obj;
        }
    }
}
