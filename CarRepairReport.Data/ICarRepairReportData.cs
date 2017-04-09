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

    public interface ICarRepairReportData
    {
        int Commit();

        IBaseEntityRepository<ApplicationUser> AppUsers { get;}
        //IBaseEntityRepository<User> Users { get; }
        IEntityRepository<IdentityRole> Roles { get; }
        IBaseEntityRepository<Language> Languages { get; }
        IBaseEntityRepository<Address> Addresses { get; }
        IBaseEntityRepository<City> Cities { get; }
        IBaseEntityRepository<Country> Countries { get; }
        IBaseEntityRepository<User> MyUsers { get;}
        IBaseEntityRepository<UserSetting> UserSettings { get; }
        IBaseEntityRepository<LanguageValue> LanguageValues { get; }
        IBaseEntityRepository<Car> Cars { get; }
        IBaseEntityRepository<CarPart> CarParts { get; }
        IBaseEntityRepository<Engine> Engines { get; }
        IBaseEntityRepository<Gearbox> Gearboxes { get; }
        IBaseEntityRepository<Manufacturer> Manufacturers { get; }
        IBaseEntityRepository<Cost> Costs { get; }

        ApplicationDbContext Context { get; }
    }
}
