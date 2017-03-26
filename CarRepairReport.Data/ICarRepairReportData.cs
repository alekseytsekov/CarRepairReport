namespace CarRepairReport.Data
{
    using System.Data.Entity;
    using CarRepairReport.Models.AppModels;
    using CarRepairReport.Models.MVC_Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public interface ICarRepairReportData
    {
        int Commit();

        IBaseEntityRepository<ApplicationUser> Users { get;}

        IEntityRepository<IdentityRole> Roles { get; }

        IBaseEntityRepository<Language> Languages { get; }

        ApplicationDbContext Context { get; }
    }
}
