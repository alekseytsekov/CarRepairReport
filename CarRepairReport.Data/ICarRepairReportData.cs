﻿namespace CarRepairReport.Data
{
    using System.Data.Entity;
    using CarRepairReport.Models.Models;
    using CarRepairReport.Models.Models.AddressModels;
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

        ApplicationDbContext Context { get; }
    }
}
