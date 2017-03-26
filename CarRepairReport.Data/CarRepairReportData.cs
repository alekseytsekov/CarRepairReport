namespace CarRepairReport.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using CarRepairReport.Models;
    using CarRepairReport.Models.AppModels;
    using CarRepairReport.Models.MVC_Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class CarRepairReportData : ICarRepairReportData
    {
        private ApplicationDbContext context;

        private IBaseEntityRepository<ApplicationUser> users;
        private IEntityRepository<IdentityRole> roles;
        private IBaseEntityRepository<Language> languages;

        public CarRepairReportData() : this(ApplicationDbContext.Create())
        {
            
        }

        public CarRepairReportData(ApplicationDbContext context)
        {
            this.context = context;
        }

        public int Commit()
        {
            this.ApplyAuditInfoRules();

            return this.context.SaveChanges();
        }

        public virtual IBaseEntityRepository<ApplicationUser> Users
        {
            get { return this.users ?? (this.users = new BaseEntityRepository<ApplicationUser>(this.context.Users)); }
        }

        public virtual IEntityRepository<IdentityRole> Roles
        {
            get { return this.roles ?? (this.roles = new EntityRepository<IdentityRole>(this.context.Roles)); }
        }

        public virtual IBaseEntityRepository<Language> Languages
        {
            get { return this.languages ?? (this.languages = new BaseEntityRepository<Language>(this.context.Languages)); }
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
