using BravoNews.Data;
using BravoNews.Models;
using Microsoft.AspNetCore.Mvc;

namespace BravoNews.ViewComponents
{
    public class MenuCategoriesViewComponent:ViewComponent
    {
        private readonly ApplicationDbContext _db;
        
        public MenuCategoriesViewComponent(ApplicationDbContext db)
        {
            _db= db;
        }
       public IViewComponentResult Invoke()
        {
            List<Category> dbCategories= _db.Categories.ToList();
            return View("Index", dbCategories);
        }
           
    }
}
