using BravoNews.Models.ViewModels;

namespace BravoNews.Services
{
    public interface IArticleQuery
    {
        FirstPageVM IndexQueries();
        //FirstPageVM LatestNews();
        //FirstPageVM EditorsChoice();
    }
}