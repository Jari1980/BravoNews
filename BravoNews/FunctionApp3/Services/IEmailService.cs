using TimerNewsApp.Model;

namespace TimerNewsApp.Services
{
    public interface IEmailService
    {
        Task<string> SendRegisterEmail(Email newEmail);
    }
}