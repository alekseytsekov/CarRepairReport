namespace CarRepairReport.Services.Interfaces
{
    using CarRepairReport.Models.Models;

    public interface IUserService
    {
        bool Add(User user);
        User GetUserById(string userId);
        bool Update(User user);
    }
}
