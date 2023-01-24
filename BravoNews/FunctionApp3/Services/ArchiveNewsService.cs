using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimerNewsApp.Data;

namespace TimerNewsApp.Services
{
    public class ArchiveNewsService : IArchiveNewsService
    {
        private readonly FuncDbContext _db;
        public ArchiveNewsService(FuncDbContext db)
        {
            _db = db;
        }
        public void ArchiveArticles()
        {
            if (_db.Articles.Where(x => x.DateStamp.AddDays(60) < DateTime.Now).Where(x => x.Archived == false).Any())
            {
                var listOfArticles = _db.Articles.Where(x => x.DateStamp.AddDays(60) < DateTime.Now).Where(x => x.Archived == false).ToList();
                foreach (var article in listOfArticles)
                {
                    article.Archived = true;
                    _db.SaveChanges();
                }
            }
        }
    }
}
