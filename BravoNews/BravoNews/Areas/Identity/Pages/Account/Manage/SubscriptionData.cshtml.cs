using BravoNews.Models;
using BravoNews.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BravoNews.Areas.Identity.Pages.Account.Manage
{
    public class SubscriptionData : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public SubscriptionData(ApplicationDbContext db, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            //var expDate = _db.Subscriptions.Where(x => x.User == user).FirstOrDefault().Expires;

            return Page();
        }
    }
}
