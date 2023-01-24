using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TimerNewsApp.Data;
using TimerNewsApp.Model.FuncModels;
using TimerNewsApp.Services;
using TimerNewsApp.Model;

namespace TimerNewsApp.Services
{
    public class WeeklyService : IWeeklyService
    {
        private readonly ILogger _logger;
        private readonly FuncDbContext _db;
        private readonly IStorageService _storageService;
        private readonly IEmailService _emailService;

        public WeeklyService(ILoggerFactory logger, FuncDbContext db, IStorageService storageService, IEmailService emailService)
        {
            _logger = logger.CreateLogger<DailyService>();
            _db = db;
            _storageService = storageService;
            _emailService = emailService;
        }
        public List<Email> SubscriberLetter()  //void -> Email
        {
            
            List<Email> ListOfMail = new List<Email>();
            var CustomerList = _db.Users.Where(x => x.PersonalizedNewsletter == true).ToList();
            foreach (var Customer in CustomerList)
            {
                Email newEmail = new();
                var Sweden = 0;
                var Weather = 0;
                var Sports = 0;
                var World = 0;
                var Local = 0;
                var Economy = 0;
                var Health = 0;
                string largest = "";
                int MaxValue = 0;
                Uri artToSendXs;
                Uri artToSendXs2;
                Uri artToSendXs3;

                if (_db.Like.Where(x => x.User.Email == Customer.Email).Any())
                {
                    if (_db.Like.Where(x => x.Article.CategoryId == 1) != null)
                    {
                        Sweden = _db.Like.Where(y =>y.User.Email == Customer.Email).Where(x => x.Article.CategoryId == 1).Count();
                    }
                    if (_db.Like.Where(x => x.Article.CategoryId == 2) != null)
                    {
                        Weather = _db.Like.Where(y => y.User.Email == Customer.Email).Where(x => x.Article.CategoryId == 2).Count();
                        if (Sweden > Weather)
                        {
                            largest = "Sweden";
                            MaxValue = Sweden;
                        }
                        else
                        {
                            largest = "Weather";
                            MaxValue = Weather;
                        }
                    }
                    if (_db.Like.Where(x => x.Article.CategoryId == 3) != null)
                    {
                        Sports = _db.Like.Where(y => y.User.Email == Customer.Email).Where(x => x.Article.CategoryId == 3).Count();
                        if (Sports > MaxValue)
                        {
                            largest = "Sports";
                            MaxValue = Sports;
                        }
                    }
                    if (_db.Like.Where(x => x.Article.CategoryId == 4) != null)
                    {
                        World = _db.Like.Where(y => y.User.Email == Customer.Email).Where(x => x.Article.CategoryId == 4).Count();
                        if (World > MaxValue)
                        {
                            largest = "World";
                            MaxValue = World;
                        }
                    }
                    if (_db.Like.Where(x => x.Article.CategoryId == 5) != null)
                    {
                        Local = _db.Like.Where(y => y.User.Email == Customer.Email).Where(x => x.Article.CategoryId == 5).Count();
                        if (Local > MaxValue)
                        {
                            largest = "Local";
                            MaxValue = Local;
                        }
                    }
                    if (_db.Like.Where(x => x.Article.CategoryId == 6) != null)
                    {
                        Economy = _db.Like.Where(y => y.User.Email == Customer.Email).Where(x => x.Article.CategoryId == 6).Count();
                        if (Economy > MaxValue)
                        {
                            largest = "Economy";
                            MaxValue = Economy;
                        }
                    }
                    if (_db.Like.Where(x => x.Article.CategoryId == 7) != null)
                    {
                        Health = _db.Like.Where(y => y.User.Email == Customer.Email).Where(x => x.Article.CategoryId == 7).Count();
                        if (Health > MaxValue)
                        {
                            largest = "Health";
                            MaxValue = Health;
                        }
                    }
                    //Uri artToSendXs;
                    if (MaxValue > 0)
                    {
                        var cat = _db.Categories.Where(x => x.Name == largest).FirstOrDefault();
                        var artChoice = _db.Articles.Where(x => x.Category.Name == cat.Name).ToList().OrderByDescending(x => x.DateStamp).Take(3);
                      
                        int counter = 1;
                        foreach (var item in artChoice)
                        {
                            if (counter == 1)
                            {

                                artToSendXs = _storageService.GetBlob(item.FileName, "news-images-xs", "/" + item.CategoryId);
                                
                                newEmail.Blob = artToSendXs;
                            }
                            if (counter == 2)
                            {
                                artToSendXs2 = _storageService.GetBlob(item.FileName, "news-images-xs", "/" + item.CategoryId);
                               
                                newEmail.Blob2 = artToSendXs2;
                            }
                            else
                            {
                                artToSendXs3 = _storageService.GetBlob(item.FileName, "news-images-xs", "/" + item.CategoryId);
                                newEmail.Blob3 = artToSendXs3;

                            }
                            counter++;

                        }

                        newEmail.SubscriberEmail = _db.Users.Where(x => x.UserName == Customer.UserName).FirstOrDefault().Email;
                        var firstName = _db.Users.Where(x => x.UserName == Customer.Email).FirstOrDefault().FirstName;
                        var lastName = _db.Users.Where(x => x.UserName == Customer.Email).FirstOrDefault().LastName;
                        newEmail.SubcriberName = firstName + " " + lastName;

                        int counter2 = 1;
                        foreach (var item in artChoice)
                        {
                            if (counter2 == 1)
                            {
                                newEmail.ContentSummary = item.ContentSummary;
                                newEmail.Headline = item.Headline;
                                
                            }
                            if (counter2 == 2)
                            {
                                newEmail.ContentSummary2 = item.ContentSummary;
                                newEmail.Headline2 = item.Headline;

                            }
                            else
                            {
                                newEmail.ContentSummary3 = item.ContentSummary;
                                newEmail.Headline3 = item.Headline;
                            }
                            counter2++;

                        }

                        ListOfMail.Add(newEmail);

                         Sweden = 0;
                        Weather = 0;
                        Sports = 0;
                        World = 0;
                        Local = 0;
                        Economy = 0;
                        Health = 0;
                        largest = "";
                        MaxValue = 0;

                    }
                }
                else
                {
                    var artChoice = _db.Articles.OrderByDescending(x => x.DateStamp).Take(3);
           
                    int counter = 1;
                    foreach(var item in artChoice)
                    {
                        if(counter == 1)
                        {
                            
                            artToSendXs = _storageService.GetBlob(item.FileName, "news-images-xs", "/" + item.CategoryId);
                           
                            newEmail.Blob = artToSendXs;
                        }
                        if(counter == 2)
                        {
                            artToSendXs2 = _storageService.GetBlob(item.FileName, "news-images-xs", "/" + item.CategoryId);
                           
                            newEmail.Blob2 = artToSendXs2;
                        }
                        else
                        {
                            artToSendXs3 = _storageService.GetBlob(item.FileName, "news-images-xs", "/" + item.CategoryId);
                            newEmail.Blob3 = artToSendXs3;

                        }
                        counter++;

                    }

                    newEmail.SubscriberEmail = _db.Users.Where(x => x.UserName == Customer.UserName).FirstOrDefault().Email;
                    var firstName = _db.Users.Where(x => x.UserName == Customer.Email).FirstOrDefault().FirstName;
                    var lastName = _db.Users.Where(x => x.UserName == Customer.Email).FirstOrDefault().LastName;
                    newEmail.SubcriberName = firstName + " " + lastName;

                    int counter2 = 1;
                    foreach(var item in artChoice)
                    {
                        if(counter2 == 1)
                        {
                            newEmail.ContentSummary = item.ContentSummary;
                            newEmail.Headline = item.Headline;

                        }
                        if(counter2 == 2)
                        {
                            newEmail.ContentSummary2 = item.ContentSummary;
                            newEmail.Headline2 = item.Headline;

                        }
                        else
                        {
                            newEmail.ContentSummary3 = item.ContentSummary;
                            newEmail.Headline3 = item.Headline;

                        }
                        counter2++;

                    }

                    ListOfMail.Add(newEmail);
                    
                    Sweden = 0;
                    Weather = 0;
                    Sports = 0;
                    World = 0;
                    Local = 0;
                    Economy = 0;
                    Health = 0;
                    largest = "";
                    MaxValue = 0;


                }
                //ListOfMail.Add(newEmail);
            }
            return ListOfMail;
        }
    }
}
