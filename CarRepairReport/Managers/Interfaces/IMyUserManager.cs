namespace CarRepairReport.Managers.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web;
    using CarRepairReport.Models.BindingModels;
    using CarRepairReport.Models.Dtos;
    using CarRepairReport.Models.Models;
    using CarRepairReport.Models.Models.UserModels;
    using CarRepairReport.Models.ViewModels;
    using CarRepairReport.Models.ViewModels.Commons;
    using CarRepairReport.Models.ViewModels.UserVms;

    public interface IMyUserManager
    {
        Task CreateMyUserAsync(ApplicationUser appUser, HttpContextBase httpContext);
        //void CreateMyUserAsync(ApplicationUser appUser, HttpContextBase httpContext);
        UserProfileVm GetUserProfileByAppUserId(string appUserId);
        bool AddUserDetails(UserProfileBm bm, string appUserId);
        EditUserVm GetEditModelByAppId(string appUserId);

        bool EditUserPersonalDetails(EditUserBm bm, string appUserId);
        ResultDto RegisterVehicleService(CreateCarServiceBm bm, string appUserId);
        ICollection<MembershipInvitationVm> GetInvitations(string email);
    }
}
