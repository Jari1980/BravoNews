using BravoNews.Data;
using BravoNews.Models;
using Microsoft.AspNetCore.Identity;


namespace BravoNews.Services
{
    public class LikeService : ILikeService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public LikeService(ApplicationDbContext db, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void LikeArticle(Article art, int Id, string userId)
        {
            if (_db.Like.Where(x => x.ArticleId == Id).Where(y => y.UserId == userId).Any() == false)
            {
                Like like = new Like();
                like.ArticleId = Id;
                like.Liked = true;
                like.Date = DateTime.Now;
                like.UserId = userId;
                _db.Like.Add(like);
                art.Likes++;
                _db.SaveChanges();
            }
        }
        public void NoLikeArticle(Article art, int Id, string userId)
        {
            if (_db.Like.Where(x => x.ArticleId == Id).Where(y => y.UserId == userId).Any() == false)
            {
                Like like = new Like();
                like.ArticleId = Id;
                like.Liked = false;
                like.Date = DateTime.Now;
                like.UserId = userId;
                _db.Like.Add(like);
                art.Likes--;
                _db.SaveChanges();
            }
        }
    }
}
