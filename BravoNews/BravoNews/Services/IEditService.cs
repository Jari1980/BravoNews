using BravoNews.Models;

namespace BravoNews.Services
{
    public interface IEditService
    {
        Article Edit(int Id);

        void Edit(DateTime DateStamp, string LinkText, string HeadLine, string ContentSummary,
                                  string Content, int Views, int Likes, Uri ImageLink,
                                  string FileName, int CategoryId, int Id);
    }
}