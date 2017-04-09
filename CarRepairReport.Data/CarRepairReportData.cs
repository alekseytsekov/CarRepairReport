namespace CarRepairReport.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using CarRepairReport.Models;
    using CarRepairReport.Models.Models;
    using CarRepairReport.Models.Models.AddressModels;
    using CarRepairReport.Models.Models.CarComponents;
    using CarRepairReport.Models.Models.CommonModels;
    using CarRepairReport.Models.Models.LanguageModels;
    using CarRepairReport.Models.Models.UserModels;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class CarRepairReportData : ICarRepairReportData
    {
        private ApplicationDbContext context;

        private IBaseEntityRepository<ApplicationUser> appUsers;
        private IBaseEntityRepository<User> users;
        private IEntityRepository<IdentityRole> roles;
        private IBaseEntityRepository<Language> languages;
        private IBaseEntityRepository<Address> addresses;
        private IBaseEntityRepository<City> cities;
        private IBaseEntityRepository<Country> countries;
        private IBaseEntityRepository<UserSetting> userSettings;
        private IBaseEntityRepository<LanguageValue> languageValues;

        private IBaseEntityRepository<Car> cars;
        private IBaseEntityRepository<CarPart> carParts;
        private IBaseEntityRepository<Engine> engine;
        private IBaseEntityRepository<Gearbox> gearbox;
        private IBaseEntityRepository<Manufacturer> manufacturer;
        private IBaseEntityRepository<Cost> costs;

        //public CarRepairReportData() : this(ApplicationDbContext.Create())
        //{

        //}

        public CarRepairReportData(ApplicationDbContext context)
        {
            this.context = context;
        }

        public int Commit()
        {
            this.ApplyAuditInfoRules();
            
            return this.context.SaveChanges();
        }

        public virtual IBaseEntityRepository<ApplicationUser> AppUsers
        {
            get { return this.appUsers ?? (this.appUsers = new BaseEntityRepository<ApplicationUser>(this.context.Users)); }
        }
        
        public virtual IBaseEntityRepository<User> MyUsers
        {
            get { return this.users ?? (this.users = new BaseEntityRepository<User>(this.context.MyUsers)); }
        }

        public virtual IEntityRepository<IdentityRole> Roles
        {
            get { return this.roles ?? (this.roles = new EntityRepository<IdentityRole>(this.context.Roles)); }
        }

        public virtual IBaseEntityRepository<Language> Languages
        {
            get { return this.languages ?? (this.languages = new BaseEntityRepository<Language>(this.context.Languages)); }
        }

        public IBaseEntityRepository<Address> Addresses
        {
            get { return this.addresses ?? (this.addresses = new BaseEntityRepository<Address>(this.context.Addresses)); }
        }

        public IBaseEntityRepository<City> Cities
        {
            get { return this.cities ?? (this.cities = new BaseEntityRepository<City>(this.context.Cities)); }
        }

        public IBaseEntityRepository<Country> Countries
        {
            get { return this.countries ?? (this.countries = new BaseEntityRepository<Country>(this.context.Countries)); }
        }

        public IBaseEntityRepository<UserSetting> UserSettings
        {
            get { return this.userSettings ?? (this.userSettings = new BaseEntityRepository<UserSetting>(this.context.UserSettings)); }
        }

        public IBaseEntityRepository<LanguageValue> LanguageValues
        {
            get { return this.languageValues ?? (this.languageValues = new BaseEntityRepository<LanguageValue>(this.context.LanguageValues)); }
        }

        public IBaseEntityRepository<Car> Cars
        {
            get { return this.cars ?? (this.cars = new BaseEntityRepository<Car>(this.context.Cars)); }
        }

        public IBaseEntityRepository<CarPart> CarParts
        {
            get { return this.carParts ?? (this.carParts = new BaseEntityRepository<CarPart>(this.context.CarParts)); }
        }

        public IBaseEntityRepository<Engine> Engines
        {
            get { return this.engine ?? (this.engine = new BaseEntityRepository<Engine>(this.context.Engines)); }
        }

        public IBaseEntityRepository<Gearbox> Gearboxes
        {
            get { return this.gearbox ?? (this.gearbox = new BaseEntityRepository<Gearbox>(this.context.Gearboxs)); }
        }

        public IBaseEntityRepository<Manufacturer> Manufacturers
        {
            get { return this.manufacturer ?? (this.manufacturer = new BaseEntityRepository<Manufacturer>(this.context.Manufacturers)); }
        }

        public IBaseEntityRepository<Cost> Costs
        {
            get { return this.costs ?? (this.costs = new BaseEntityRepository<Cost>(this.context.Costs)); }
        }

        public ApplicationDbContext Context { get { return this.context; } }

        private void ApplyAuditInfoRules()
        {
            // @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.Context.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IBaseModel && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IBaseModel)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }

    }
}
