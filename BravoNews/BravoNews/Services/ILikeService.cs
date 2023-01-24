using BravoNews.Models;

namespace BravoNews.Services
{
    public interface ILikeService
    {
        void LikeArticle(Article art, int Id, string userId);
        void NoLikeArticle(Article art, int Id, string userId);
    }
}