using BravoNews.Data;
using BravoNews.Models;
using BravoNews.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace BravoNews.Services
{

    public class AdminPageService : IAdminPageService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _db;

        public AdminPageService(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, ApplicationDbContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }

        public async Task AssignRoleUser(string userName, string radio)
        {
            var user = _userManager.FindByNameAsync(userName).Result;

            if (radio == "Administrator")
            {
                var result = await _userManager.AddToRoleAsync(user, "ADMINISTRATOR");
            }
            else if (radio == "Editor")
            {

                var result = await _userManager.AddToRoleAsync(user, "EDITOR");
            }
            else if (radio == "Journalist")
            {
                var result = await _userManager.AddToRoleAsync(user, "JOURNALIST");
            }
        }
        public async Task RemoveRoleUser(string userName, string radioR)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            if (radioR == "Administrator")
            {
                var resultR = await _userManager.RemoveFromRoleAsync(user, "ADMINISTRATOR");
            }
            else if (radioR == "Editor")
            {
                var resultR = await _userManager.RemoveFromRoleAsync(user, "EDITOR");
            }
            else if (radioR == "Journalist")
            {
                var resultR = await _userManager.RemoveFromRoleAsync(user, "JOURNALIST");
            }
        }
        public int[] SubscriberStat()
        {
            int[] subs = new int[30];
            for (int i = 0; i > -30; i--)
            {
                subs[-i] = _db.Subscriptions.Where(x => x.Created.Date == DateTime.Now.AddDays(i).Date).Count();
            }

            return subs;
        }

        //List<DataPointVM> dataPoints = new List<DataPointVM>();
        public List<DataPointVM> ArticleStat()
        {
            List<DataPointVM> dataPoints = new List<DataPointVM>();

            dataPoints.Add(new DataPointVM("Sweden", _db.Articles.Where(y => y.CategoryId == 1).Where(x => x.DateStamp > DateTime.Now.AddMonths(-4)).Count()));
            dataPoints.Add(new DataPointVM("World", _db.Articles.Where(y => y.CategoryId == 4).Where(x => x.DateStamp > DateTime.Now.AddMonths(-4)).Count()));
            dataPoints.Add(new DataPointVM("Health", _db.Articles.Where(y => y.CategoryId == 7).Where(x => x.DateStamp > DateTime.Now.AddMonths(-4)).Count()));
            dataPoints.Add(new DataPointVM("Weather", _db.Articles.Where(y => y.CategoryId == 2).Where(x => x.DateStamp > DateTime.Now.AddMonths(-4)).Count()));
            dataPoints.Add(new DataPointVM("Economy", _db.Articles.Where(y => y.CategoryId == 6).Where(x => x.DateStamp > DateTime.Now.AddMonths(-4)).Count()));
            dataPoints.Add(new DataPointVM("Sport", _db.Articles.Where(y => y.CategoryId == 3).Where(x => x.DateStamp > DateTime.Now.AddMonths(-4)).Count()));
            dataPoints.Add(new DataPointVM("Local", _db.Articles.Where(y => y.CategoryId == 5).Where(x => x.DateStamp > DateTime.Now.AddMonths(-4)).Count()));
            dataPoints.Add(new DataPointVM("Advertisement", _db.Articles.Where(y => y.CategoryId == 15).Where(x => x.DateStamp > DateTime.Now.AddMonths(-4)).Count()));

            return dataPoints;
        }
    }
}

