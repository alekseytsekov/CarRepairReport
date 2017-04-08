namespace CarRepairReport.Services.Interfaces
{
    using System.Collections.Generic;
    using CarRepairReport.Models.Models;
    using CarRepairReport.Models.Models.UserModels;

    public interface IUserService
    {
        bool Add(User user);
        User GetUserById(string userId);
        bool Update(User user);
        User GetUserByAppId(string appUserId);
        bool IsUserExists(string appUserId);
        bool UpdatePersonalInfo(string firstName, string lastName, string appUserId);

        IEnumerable<User> GetAllUsers();
    }
}
