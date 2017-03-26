namespace CarRepairReport.Data
{
    using System.Data.Entity;
    using CarRepairReport.Models.AppModels;
    using CarRepairReport.Models.MVC_Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("CarRepairReport", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual IDbSet<Language> Languages { get; set; }
        
    }
}