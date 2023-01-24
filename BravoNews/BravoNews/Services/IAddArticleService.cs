using BravoNews.Models.ViewModels;

namespace BravoNews.Services
{
    public interface IAddArticleService
    {
        AddArticleVM AddArticle();
        void AddArticlePost(AddArticleVM newArticle);

    }
}