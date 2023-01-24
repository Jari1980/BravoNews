using BravoNews.Data;
using BravoNews.Models;


namespace BravoNews.Services
{

    public class ArticlesService : IArticlesService
    {

        private readonly ApplicationDbContext _db;

        public ArticlesService(ApplicationDbContext db)
        {

            _db = db;

        }

        public List<Article> Articles(int Id)
        {
            var obj = _db.Articles.Where(x => x.Id == Id).ToList();
            obj.FirstOrDefault().Views++;
            _db.SaveChanges();

            return obj;

        }


    }
}
