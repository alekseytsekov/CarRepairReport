namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CarRepairReport.Models;
    using CarRepairReport.Models.Models;
    using CarRepairReport.Models.Models.AddressModels;
    using CarRepairReport.Models.Models.LanguageModels;

    internal sealed class Configuration : DbMigrationsConfiguration<CarRepairReport.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "CarRepairReport.Data.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Languages.Any())
            {
                var enLang = new Language()
                {
                    Name = "English",
                    TwoLetterCode = "en",
                    CreatedOn = DateTime.UtcNow,
                    IsSystemLanguage = true
                };

                var bgLang = new Language()
                {
                    Name = "Български",
                    TwoLetterCode = "бг",
                    CreatedOn = DateTime.UtcNow,
                    IsSystemLanguage = true
                };

                var ruLang = new Language()
                {
                    Name = "Русский",
                    TwoLetterCode = "ру",
                    CreatedOn = DateTime.UtcNow,
                    IsSystemLanguage = true
                };

                context.Languages.Add(enLang);
                context.Languages.Add(bgLang);
                context.Languages.Add(ruLang);

                context.SaveChanges();
            }

            if (!context.Countries.Any())
            {
                var country = new Country()
                {
                    Name = "My Country.",
                    CreatedOn = DateTime.UtcNow,
                    CountryCode = "MC"
                };

                var city = new City()
                {
                    Name = "My City",
                    Country = country,
                    CreatedOn = DateTime.UtcNow
                };

                country.Cities.Add(city);
                context.Cities.Add(city);

                var address = new Address()
                {
                    CreatedOn = DateTime.UtcNow,
                    City = city,
                    AddressType = AddressType.Home,
                    IsPrimary = true,
                    StreetName = "My Street"
                };

                city.Addresses.Add(address);
                context.Addresses.Add(address);

                context.SaveChanges();
            }
        }
    }
}
