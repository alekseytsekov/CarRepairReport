namespace CarRepairReport.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CarRepairReport.Models.Models;
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
        }
    }
}
