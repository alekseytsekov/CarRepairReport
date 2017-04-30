namespace CarRepairReport.Managers
{
    using System.Collections.Generic;
    using System.Web;
    using AutoMapper;
    using CarRepairReport.Data;
    using CarRepairReport.Globals;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.Dtos;
    using CarRepairReport.Models.Models.LanguageModels;
    using CarRepairReport.Services;
    using CarRepairReport.Services.Interfaces;

    public class CacheManager : ICacheManager
    {
        private ILanguageService languageService;

        public CacheManager(ILanguageService languageService)
        {
            this.languageService = languageService;
        }
        
        public void LoadLanguageResources()
        {
            //var langService = new LanguageService(new CarRepairReportData(ApplicationDbContext.Create()));

            //var allResources = langService.GetAllResources();

            var allResources = this.languageService.GetAllResources();

            var cacheModels = Mapper.Map<IEnumerable<LanguageValue>, IEnumerable<LanguageDto>>(allResources);

            HttpContext.Current.Cache[CRRConfig.LanguageResourcesCacheCollection] = cacheModels;
        }
    }
}