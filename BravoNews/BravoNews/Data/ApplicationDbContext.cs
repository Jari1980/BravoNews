using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BravoNews.Models;

namespace BravoNews.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BravoNews.Models.Article> Articles { get; set; }
        public DbSet<BravoNews.Models.Category> Categories { get; set; }
        public DbSet<BravoNews.Models.Subscription> Subscriptions { get; set; }
        public DbSet<BravoNews.Models.SubscriptionType> SubscriptionTypes { get; set; }
        public DbSet<BravoNews.Models.User> Users { get; set; }
        public DbSet<BravoNews.Models.Like> Like { get; set; }

    }
}