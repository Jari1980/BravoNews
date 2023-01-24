using BravoNews.Data;
using BravoNews.Services;
using Microsoft.AspNetCore.Mvc;

namespace BravoNews.ViewComponents
{
    public class MenuWeatherDataViewComponent:ViewComponent
    {
        private readonly ApplicationDbContext _db;
        private readonly IWeatherForecastService _weatherForecastService;

            public MenuWeatherDataViewComponent(ApplicationDbContext db, IWeatherForecastService weatherForecastService)
        {
            _db = db;

            _weatherForecastService = weatherForecastService; 
        }

        public IViewComponentResult Invoke(string city)
        {
            var data = _weatherForecastService.GetForecast(city);

            return View("Index",data);
        }
    }
}
