using BravoNews.Models;
using BravoNews.Models.ViewModels;

namespace BravoNews.Services
{
    public interface IArticleService
    {
        void SaveArticle(AddArticleVM article, Uri blobUri);
        List<Article> GetAllArticles();
        List<Article> GetAllArticles(int id);
       
    }
}
