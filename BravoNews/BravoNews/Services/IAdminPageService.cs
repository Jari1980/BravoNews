using BravoNews.Models.ViewModels;

namespace BravoNews.Services
{
    public interface IAdminPageService
    {
        Task AssignRoleUser(string userName, string radio);
        Task RemoveRoleUser(string userName, string radioR);
        int[] SubscriberStat();
        List<DataPointVM> ArticleStat();
    }
}