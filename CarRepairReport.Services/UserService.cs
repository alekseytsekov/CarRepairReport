namespace CarRepairReport.Services
{
    using CarRepairReport.Data;
    using CarRepairReport.Models.Models;
    using CarRepairReport.Services.Interfaces;
    public class UserService : Service, IUserService
    {
        public UserService(ICarRepairReportData context) : base(context)
        {
        }

        public bool Add(User user)
        {
            this.context.MyUsers.Add(user);

            this.context.Commit();

            return true;
        }

        public User GetUserById(string userId)
        {
            var user = this.context.MyUsers.FirstOrDefault(x => x.ApplicationUserId == userId);

            return user;
        }
    }
}
