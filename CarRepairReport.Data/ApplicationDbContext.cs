namespace CarRepairReport.Data
{
    using System.Data.Entity;
    using CarRepairReport.Models.Models;
    using CarRepairReport.Models.Models.AddressModels;
    using CarRepairReport.Models.Models.CarComponents;
    using CarRepairReport.Models.Models.CommonModels;
    using CarRepairReport.Models.Models.LanguageModels;
    using CarRepairReport.Models.Models.UserModels;
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>()
            //    .HasOptional(u => u.VehicleService) 
            //    .WithRequired(vs => vs.User)
            //    .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public virtual IDbSet<Language> Languages { get; set; }
        public virtual IDbSet<Address> Addresses { get; set; }
        public virtual IDbSet<City> Cities { get; set; }
        public virtual IDbSet<Country> Countries { get; set; }
        public virtual IDbSet<User> MyUsers { get; set; }
        public virtual IDbSet<UserSetting> UserSettings { get; set; }
        public virtual IDbSet<LanguageValue> LanguageValues { get; set; }
        public virtual IDbSet<Car> Cars { get; set; }
        public virtual IDbSet<CarPart> CarParts { get; set; }
        public virtual IDbSet<Engine> Engines { get; set; }
        public virtual IDbSet<Gearbox> Gearboxs { get; set; }
        public virtual IDbSet<Manufacturer> Manufacturers { get; set; }
        public virtual IDbSet<Cost> Costs { get; set; }
        public virtual IDbSet<VehicleService> VehicleServices { get; set; }
        public virtual IDbSet<ServiceRating> ServiceRatings { get; set; }
    }
}