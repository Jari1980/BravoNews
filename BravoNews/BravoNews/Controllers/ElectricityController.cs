using BravoNews.Models;
using BravoNews.Services;
using Microsoft.AspNetCore.Mvc;

namespace BravoNews.Controllers
{
    public class ElectricityController : Controller
    {

        private readonly IElectricityService _electricityService;

        public ElectricityController(IElectricityService ElectricityService)
        {

            _electricityService = ElectricityService;   

        }

        public IActionResult Index()
        {

            var data = _electricityService.GetElectricityPrice();
            return View("Index", data);

        }



    }
}
