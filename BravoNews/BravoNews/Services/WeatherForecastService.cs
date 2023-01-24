using BravoNews.Models;
using Microsoft.AspNetCore.Mvc;

namespace BravoNews.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private const string BaseUrl = "https://weatherapi.dreammaker-it.se";
        private readonly HttpClient _http;

        public WeatherForecastService(HttpClient http)
        {
            _http = http;
        }

       

        public WeatherForecast GetForecast(string city, string lang = "en")
        {
            var response = _http.GetAsync($"{BaseUrl}/forecast?city={city}&lang={lang}").Result;
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var data = response.Content.ReadFromJsonAsync<WeatherForecast>().Result;
            return data;
        }
    }
}
