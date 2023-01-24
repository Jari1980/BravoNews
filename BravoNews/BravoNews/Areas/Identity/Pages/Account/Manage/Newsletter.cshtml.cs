using BravoNews.Models;
using BravoNews.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BravoNews.Areas.Identity.Pages.Account.Manage
{
    public class Newsletter : PageModel
    {
        [BindProperty]
        public bool SubscribeNews { get; set; }

        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public Newsletter(ApplicationDbContext db, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
           

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userManager.GetUserAsync(User);
            if(SubscribeNews == true)
            {
                user.PersonalizedNewsletter = true;
            }
            else
            {
                user.PersonalizedNewsletter = false;
            }
            _db.SaveChanges();

            return Page();
        }

        //public async Task<IActionResult> UnsubcribeNewsletter()
        //{
        //    var user = await _userManager.FindByIdAsync(userId);

        //    return View();
        //}

    }
}
