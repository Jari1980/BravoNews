using BravoNews.Data;
using BravoNews.Models;
using BravoNews.Models.ViewModels;
using AutoMapper;

namespace BravoNews.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public ArticleService(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public void SaveArticle(AddArticleVM newArticle, Uri blobUri)
        {
            Article dbArticle = _mapper.Map<Article>(newArticle);

            //dbArticle.Category.Name = _db.Categories.Find(Convert.ToInt32(newArticle.CategoryId)).Name;
            //dbArticle.Category.Id = _db.Categories.Find(Convert.ToInt32(newArticle.CategoryId)).Id;


            dbArticle.Category = _db.Categories.Find(newArticle.CategoryId);

            dbArticle.ImageLink = blobUri;
            dbArticle.DateStamp = DateTime.Now;
            _db.Add(dbArticle);
            _db.SaveChanges();
        }

        public List<Article> GetAllArticles()
        {
            return _db.Articles.ToList();
        }
        public List<Article> GetAllArticles(int id)
        {
            return _db.Articles.Where(a => a.CategoryId== id).ToList();
        }
        
        //public void AddCategories()
        //{
        //    List<Category> categories = new List<Category>();
        //    Category category = new Category();
        //    category.Name = "Sweden";
        //    category.Name = "Weather";
        //    category.Name = "Sports";
        //    category.Name = "World";
        //    category.Name = "Local";
        //    category.Name = "Economy";
        //    category.Name = "Health";

        //    _db.AddRange(categories);
        //    _db.SaveChanges();

        //}
    }
}
