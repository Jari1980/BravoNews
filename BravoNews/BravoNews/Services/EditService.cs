using BravoNews.Data;
using BravoNews.Models;
using Microsoft.AspNetCore.Mvc;

namespace BravoNews.Services
{
    public class EditService : IEditService
    {
        
        private readonly ApplicationDbContext _db;

        public EditService(ApplicationDbContext db)
        {
            _db = db;
        }
        public Article Edit(int Id)
        {
            var art = _db.Articles.Where(x => x.Id == Id).FirstOrDefault();
           
            return art;
        }


        public void Edit(DateTime DateStamp, string LinkText, string HeadLine, string ContentSummary,
                                       string Content, int Views, int Likes, Uri ImageLink,
                                       string FileName, int CategoryId, int Id)
        {
            Article art = _db.Articles.Where(x => x.Id == Id).FirstOrDefault();
            art.DateStamp = DateStamp;
            art.LinkText = LinkText;
            art.Headline = HeadLine;
            art.ContentSummary = ContentSummary;
            art.Content = Content;
            art.Views = Views;
            art.Likes = Likes;
            art.ImageLink = ImageLink;
            art.FileName = FileName;
            art.CategoryId = CategoryId;
            _db.SaveChanges();

            return;
        }
    }
}
