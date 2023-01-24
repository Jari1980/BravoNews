namespace BravoNews.Services
{
    public interface IUserService
    {
        void AddRole(string roleName);
        void MakeUserAdmin(string userId);
        void MakeUserPremium(string userId);
        void MakeUserRegistered(string userId);
    }
}