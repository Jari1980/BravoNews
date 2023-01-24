using BravoNews.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BravoNews.Services;
using BravoNews.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using BravoNews.Data;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;
using SixLabors.ImageSharp.ColorSpaces;
using Microsoft.Extensions.Azure;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

//using Microsoft.EntityFrameworkCore;

namespace BravoNews.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailService _emailService;
        private readonly IStorageService _storageService;
        private readonly IArticleService _articleService;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly ILikeService _likeService;
        private readonly IArticleQuery _articleQuery;
        private readonly IAdminPageService _adminPageService;
        private readonly IAddArticleService _addArticleService;
        private readonly IEditService _editService;
        private readonly IArticlesService _articlesService;
        private readonly ICreateSubscriptionService _createSubscriptionService;
        private readonly HttpClient _http;
        public HomeController(ILogger<HomeController> logger, IEmailService emailService, IStorageService storageService,
                                IArticleService articleService, IConfiguration configuration, RoleManager<IdentityRole> roleManager,
                                UserManager<User> userManager, ApplicationDbContext db, ILikeService likeService, IArticlesService articlesService,
                                IArticleQuery articleQuery, IAdminPageService adminPageService, IAddArticleService addArticleService, IEditService editService,
                                ICreateSubscriptionService createSubscriptionService, HttpClient http)

        {

            _logger = logger;
            _emailService = emailService;
            _storageService = storageService;
            _articleService = articleService;
            _configuration = configuration;
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
            _likeService = likeService;
            _articleQuery = articleQuery;
            _adminPageService = adminPageService;
            _addArticleService = addArticleService;
            _editService = editService;
            _articlesService = articlesService;
            _createSubscriptionService = createSubscriptionService;
            _http = http;

        }


        public IActionResult Index()
        {
            FirstPageVM obj = new FirstPageVM();
            obj = _articleQuery.IndexQueries();

            return View(obj);
        }

        public IActionResult Articles(int Id)
        {

            var obj = _articlesService.Articles(Id);
            //var obj = _db.Articles.Where(x => x.Id == Id).ToList();
            //obj.FirstOrDefault().Views++;
            //_db.SaveChanges();

            return View(obj);
        }

        public IActionResult likeArticle(int Id)
        {
            Article art = _db.Articles.Where(x => x.Id == Id).FirstOrDefault();
            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                _likeService.LikeArticle(art, Id, userId);
            }
            var count = art.Likes;

            return Json(new { Value = count });
        }

        public IActionResult nolikeArticle(int Id)
        {
            Article art = _db.Articles.Where(x => x.Id == Id).FirstOrDefault();
            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                _likeService.NoLikeArticle(art, Id, userId);
            }
            var count = art.Likes;

            return Json(new { Value = count });
        }

        public IActionResult EditArticle()
        {
            var articles = _db.Articles;

            return View(articles);
        }

        public IActionResult EditorsChoice(int Id)
        {
            var art = _db.Articles.Where(x => x.Id == Id).FirstOrDefault();
            art.DateStampEditorsChoice = DateTime.Now;
            var date = art.DateStampEditorsChoice;
            _db.SaveChanges();

            return RedirectToAction("EditArticle");
        }

        public IActionResult Edit(int Id)
        {
            var art = _editService.Edit(Id);

            return View(art);
        }

        [HttpPost]
        public IActionResult Edit(DateTime DateStamp, string LinkText, string HeadLine, string ContentSummary,
                                    string Content, int Views, int Likes, Uri ImageLink,
                                    string FileName, int CategoryId, int Id)
        {
            _editService.Edit(DateStamp, LinkText, HeadLine, ContentSummary,
                                        Content, Views, Likes, ImageLink,
                                      FileName, CategoryId, Id);
            return View();
        }

        public IActionResult Delete(int Id)
        {
            var art = _db.Articles.Where(x => x.Id == Id).FirstOrDefault();

            return View(art);
        }

        [HttpPost]
        public IActionResult Delete(Article art)
        {
            _db.Articles.Remove(art);
            _db.SaveChanges();

            return RedirectToAction("EditArticle");
        }


        public IActionResult Search(string search)
        {
            var artsearch = new List<Article>();
            var art = from a in _db.Articles
                      select a;
            if (!String.IsNullOrEmpty(search))
            {
                artsearch = art.Where(b => b.Headline.Contains(search) || b.Content.Contains(search)
                || b.ContentSummary.Contains(search)).ToList();

            }

            return View(artsearch);
        }


        public IActionResult AddNewRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewRole(string roleName)
        {

            if (roleName != null || roleName != "")
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);

                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            return RedirectToAction("Index");
        }


        //hejhopp
        ////[HttpPost]
        ////public IActionResult SetAdmin(User admin)
        ////{
        ////    var admin = Activator.CreateInstance<User>();
        ////    _userManager.AddToRoleAsync(admin, "Administrator");


        ////    RedirectToAction("AdminPages");  //Change to proper
        ////}

        ////public async Task<IActionResult> SetPremium()
        ////{
        ////    var premium = Activator.CreateInstance<User>();
        ////    _userManager.AddToRoleAsync(premium, "Premium");


        ////    return View(Index);  //Change to proper
        ////}

        ////public async Task<IActionResult> SetRegistered()
        ////{
        ////    var registered = Activator.CreateInstance<User>();
        ////    _userManager.AddToRoleAsync(registered, "Registered");


        ////    return View(Index);  //Change to proper
        ////}


        //        if (!roleExist)
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole(roleName));
        //        }
        //    }
        //    return RedirectToAction("Index", "Home");
        //}


        public async Task<IActionResult> AddAdministrationAndUserRoles()
        {
            string role1 = "Administrator";
            string role2 = "Premium";
            string role3 = "Registered";

            string[] roleNames = { role1, role2, role3 };
            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            return View();
        }

        public IActionResult PresentNews()
        {
            return View();
        }

        public IActionResult MyPage()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AdminPages()
        {
            //var obj = new List<UserVM>();

            //foreach (var user in _userManager.Users.ToList())
            //{
            //    if (_userManager.GetRolesAsync(user).Result.ToList().Count() > 0) {

            //        obj.Add(new UserVM
            //        {
            //            UserName = _db.Users.Where(x => x.UserName == user.UserName).First().UserName,
            //            Roles = _userManager.GetRolesAsync(user).Result.ToList()
            //        });

            //    }
            //}

            return View();
        }

        public IActionResult RoleAssignment()
        {
            var obj = new List<UserVM>();

            foreach (var user in _userManager.Users.ToList())
            {
                if (_userManager.GetRolesAsync(user).Result.ToList().Count() > 0)
                {
                    obj.Add(new UserVM
                    {
                        UserName = _db.Users.Where(x => x.UserName == user.UserName).First().UserName,
                        Roles = _userManager.GetRolesAsync(user).Result.ToList()
                    });
                }
            }
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> RoleAssignment(string userName, string radio, string radioR)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            await _adminPageService.AssignRoleUser(userName, radio);
            await _adminPageService.RemoveRoleUser(userName, radioR);

            var obj = new List<UserVM>();
            foreach (var user1 in _userManager.Users.ToList())
            {
                if (_userManager.GetRolesAsync(user1).Result.ToList().Count() > 0)
                {
                    obj.Add(new UserVM
                    {
                        UserName = _db.Users.Where(x => x.UserName == user1.UserName).First().UserName,
                        Roles = _userManager.GetRolesAsync(user1).Result.ToList()
                    });
                }
            }
            return RedirectToAction("RoleAssignment", obj);
        }

        public IActionResult SubscriptionStatistics()
        {
            ViewBag.Message = _adminPageService.SubscriberStat();

            return View();
        }

        public IActionResult ArticleStatistics()
        {
            ViewBag.DataPoints = JsonConvert.SerializeObject(_adminPageService.ArticleStat());

            return View();
        }

        public IActionResult Create()
        {

            return RedirectToAction("AddArticle");
        }
        public IActionResult AddArticle()
        {
            var newArticle = _addArticleService.AddArticle();

            return View(newArticle);
        }

        [HttpPost]
        public IActionResult AddArticle(AddArticleVM newArticle)
        {
            _addArticleService.AddArticlePost(newArticle);

            return RedirectToAction("Index");
        }

        public IActionResult News(int id)
        {
            List<Uri> listOfBlobs = new List<Uri>();
            string blobPath = "news-images-sm";
            var categoryArticles = _articleService.GetAllArticles(id);
            var newList = categoryArticles.Where(x => x.Archived == false).OrderByDescending(a => a.DateStamp).Take(6);

            foreach (var item in newList)
            {
                item.ImageLink = _storageService.GetBlob(item.FileName, blobPath, "/" + item.CategoryId);
                listOfBlobs.Add(_storageService.GetBlob(item.FileName, blobPath, "/" + item.CategoryId));
            }
     

            //ViewBag.Message = listOfBlobs;
            return View(newList);
            //return View(categoryArticles.Where(x => x.Archived == false).OrderByDescending(a => a.DateStamp).Take(6));
        }

        public IActionResult ArchivedNews()
        {
            var archived = _db.Articles.Where(x => x.Archived == true).ToList();

            return View(archived);
        }

        public IActionResult Subscribe()
        {

            if (_userManager.GetUserId(User).Any())
            {
                var user = _userManager.GetUserName(User);
                if (_db.Subscriptions.Where(x => x.User.UserName == user).Any())
                {
                    if (_db.Subscriptions.Where(x => x.User.UserName == user).FirstOrDefault().Active == true)
                    {
                        ViewBag.Message = "You already have a subscription, please enter 'My Account - Subscriptions' for further details.";
                    }
                }
            }

            return View();
        }

        [HttpPost]
        public IActionResult Subscribe(int plan)
        {

            Subscription sub = new Subscription();
            sub.SubscriptionTypeId = plan;
            sub.Created = DateTime.Now;
            sub.User = _db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            sub.Active = true;


            if (plan == 1)
            {
                sub.Price = 5;
                sub.Expires = DateTime.Now.AddDays(30);
            }
            else
            {
                sub.Price = 50;
                sub.Expires = DateTime.Now.AddYears(1);
            }

            sub.PaymentComplete = true;
            _db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault().IsPremium = true;
            _db.Subscriptions.Add(sub);
            _db.SaveChanges();

            Email newEmail = new();
            newEmail.SubscriberEmail = _db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault().Email;
            var firstName = _db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault().FirstName;
            var lastName = _db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault().LastName;
            newEmail.SubcriberName = firstName + " " + lastName;
            newEmail.SubscriptionTypeName = "Premium";
            newEmail.Expires = sub.Expires;
            newEmail.Price = sub.Price;
            newEmail.SubscriptionTypeId = sub.SubscriptionTypeId;
            _emailService.SendRegisterEmail(newEmail);

            return RedirectToAction("ThankYou");


        }

        public IActionResult ThankYou()
        {
            return View();
        }

        public IActionResult ViewSubscriptionStatistics()
        {
            return View();
        }

        public IActionResult CreateSubscription()
        {
            //Email newEmail = new()
            //{
            //    //SubscriberEmail = "sgmohite02@gmail.com",
            //    //SubcriberName = "Sujata",
            //    //SubscriptionTypeName = "Premium"
            //};
            var newEmail = _createSubscriptionService.Subscription();
            TempData["ShowMessage"] = SendConfirmation(newEmail);
            return RedirectToAction("Privacy");
        }

        public IActionResult CancelSubscription()
        {

            return View();
        }

        public string SendConfirmation(Email newEmail)
        {
            return _emailService.SendSubscriptionEmail(newEmail).Result;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ChuckNorris()
        {
            var response = _http.GetAsync($"https://api.chucknorris.io/jokes/random").Result;
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var data = response.Content.ReadFromJsonAsync<ChuckN>().Result;
            ViewBag.Message = data.value;

            return View();
        }


        public IActionResult ContactForm()
        {

            return View();
        }
        [HttpPost]
        public IActionResult ContactForm(string emailAddress, string name, string message)
        {

            Email newEmail = new();

            newEmail.SubcriberName = name + " sent following mail from contact form: " + message + ", " + emailAddress;
            newEmail.SubscriptionTypeName = "ContactForm";
            newEmail.Expires = DateTime.Now;

            _emailService.SendRegisterEmail(newEmail);

            return RedirectToAction("MessageRecieved");
        }

        public IActionResult MessageRecieved()
        {
            return View();
        }
    }
}

    

