using BravoNews.Models;

namespace BravoNews.Services
{
    public interface IWeatherForecastService
    {
        WeatherForecast GetForecast(string city, string lang = "en");
    }
}