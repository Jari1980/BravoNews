using BravoNews.Models;

namespace BravoNews.Services

{
    public interface IArticlesService
    {

        List<Article> Articles(int Id);


    }
}