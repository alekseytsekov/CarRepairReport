namespace CarRepairReport.Managers.Interfaces
{
    using System.Threading.Tasks;
    using System.Web;
    using CarRepairReport.Models.BindingModels;
    using CarRepairReport.Models.Models;
    using CarRepairReport.Models.ViewModels;

    public interface IMyUserManager
    {
        Task CreateMyUserAsync(ApplicationUser appUser, HttpContextBase httpContext);
        //void CreateMyUserAsync(ApplicationUser appUser, HttpContextBase httpContext);
        UserProfileVm GetUserProfileByAppUserId(string userId);
        bool AddUserDetails(UserProfileBm bm, string userId);
    }
}
