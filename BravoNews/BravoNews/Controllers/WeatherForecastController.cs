using BravoNews.Services;
using Microsoft.AspNetCore.Mvc;

namespace BravoNews.Controllers
{
    public class WeatherForecastController : Controller
    {
        private readonly IWeatherForecastService _weatherForecastService;
        public WeatherForecastController( IWeatherForecastService weatherForecastService)
        {

            _weatherForecastService = weatherForecastService;
                
        }

        public IActionResult Index(string city = "Linköping", string lang = "en")
        {
            var weather = _weatherForecastService.GetForecast(city, lang);
            return View("Index",weather);
        }
    }
}
