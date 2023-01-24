using BravoNews.Data;
using BravoNews.Models;
using Microsoft.AspNetCore.Mvc;
using BravoNews.Services;



namespace BravoNews.ViewComponents
{
    public class MenuElectricityDataViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        private readonly IElectricityService _electricityService;   

        public MenuElectricityDataViewComponent(ApplicationDbContext db, IElectricityService electricityService)
        {
            _db = db;   
            _electricityService = electricityService;   

        }

        public IViewComponentResult Invoke()
        {
            var data = _electricityService.GetElectricityPrice();
            return View("Index", data);
        }


    }
}
