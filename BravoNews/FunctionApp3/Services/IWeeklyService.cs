using TimerNewsApp.Model;

namespace TimerNewsApp.Services
{
    public interface IWeeklyService
    {
        List<Email> SubscriberLetter();
    }
}