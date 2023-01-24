using Microsoft.AspNetCore.Identity;
using BravoNews.Models;
using BravoNews.Data;


namespace BravoNews.Services 
{
    public class UserService : IUserService
    {
        private ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserService(ApplicationDbContext db, UserManager<User> userManager,
                            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void AddRole(string roleName)
        {
            _roleManager.CreateAsync(new IdentityRole(roleName));
        }
        public void MakeUserAdmin(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            _userManager.AddToRoleAsync(user, "Admin");
        }
        public void MakeUserPremium(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            _userManager.AddToRoleAsync(user, "Premium");
        }
        public void MakeUserRegistered(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            _userManager.AddToRoleAsync(user, "Registered");
        }
    }
}
