using BravoNews.Data;
using Microsoft.AspNetCore.Mvc;
using BravoNews.Models.ViewModels;

namespace BravoNews.ViewComponents
{
    public class AdvertisementRightViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public AdvertisementRightViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            dbAdvertisementVM dbAdvertisement = new dbAdvertisementVM();

            dbAdvertisement.dbAdvertisement = _db.Articles.OrderByDescending(x => x).Where(x => x.CategoryId == 15).Take(6).ToList();

            //var dbAdvertisement = _db.Articles.Where(x => x.CategoryId == 15).ToList();
            return View("AdRight", dbAdvertisement);
        }
    }
}
