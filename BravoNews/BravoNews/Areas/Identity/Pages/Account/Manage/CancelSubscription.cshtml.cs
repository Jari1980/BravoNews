using System;
using System.Threading.Tasks;
using BravoNews.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using BravoNews.Data;

namespace BravoNews.Areas.Identity.Pages.Account.Manage
{
    public class CancelSubscriptionModel : PageModel
    {
        [BindProperty]
        public bool CancelSubscriptionBool { get; set; }
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;

        public CancelSubscriptionModel(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {

            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            var user = await _userManager.GetUserAsync(User);
            if (CancelSubscriptionBool)
            {
                _db.Users.Where(x => x.UserName == user.UserName).FirstOrDefault().IsPremium = false;
                _db.Subscriptions.Where(x => x.User.Email == user.Email).FirstOrDefault().Expires = DateTime.Now;
                _db.Subscriptions.Where(x => x.User.Email == user.Email).FirstOrDefault().Active = false;
                _db.SaveChanges();
                return RedirectToAction("CancelSubscription");
                //RedirectToAction("HomeController/CancelSubscription");
            }
            else
            {
                return Page();
            }
        }
    }
}




