namespace CarRepairReport.Managers.Interfaces
{
    using System.Threading.Tasks;
    using System.Web;
    using CarRepairReport.Models.Models;

    public interface IMyUserManager
    {
        Task CreateMyUserAsync(ApplicationUser appUser, HttpContextBase httpContext);
        //void CreateMyUserAsync(ApplicationUser appUser, HttpContextBase httpContext);
    }
}
