using BravoNews.Models;

namespace BravoNews.Services
{
    public interface IEmailService
    {
        Task<string> SendSubscriptionEmail(Email newEmai);
        Task<string> SendRegisterEmail(Email newEmail);
    }
}
