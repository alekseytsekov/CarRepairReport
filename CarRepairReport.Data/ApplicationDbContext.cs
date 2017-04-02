namespace CarRepairReport.Data
{
    using System.Data.Entity;
    using CarRepairReport.Models.Models;
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
        public virtual IDbSet<Address> Addresses { get; set; }
        public virtual IDbSet<City> Cities { get; set; }
        public virtual IDbSet<Country> Countries { get; set; }
        public virtual IDbSet<User> MyUsers { get; set; }
        public virtual IDbSet<UserSetting> UserSettings { get; set; }
        public virtual IDbSet<LanguageValue> LanguageValues { get; set; }
    }
}