namespace CarRepairReport.Managers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using CarRepairReport.Globals;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.Dtos;
    using CarRepairReport.Models.Models.LanguageModels;
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

            var allResources = this.languageService.GetAllResources();

            var cacheModels = Mapper.Map<IEnumerable<LanguageValue>, IEnumerable<LanguageDto>>(allResources);

            HttpContext.Current.Cache[CRRConfig.LanguageResourcesCacheCollection] = cacheModels;

            //var resources =
            //    HttpContext.Current.Cache[CRRConfig.LanguageResourcesCacheCollection] as IEnumerable<LanguageDto>;
            //resources =
            //    (IEnumerable<LanguageDto>) HttpContext.Current.Cache[CRRConfig.LanguageResourcesCacheCollection];

            //HttpContext.Current.Cache.Add(CRRConfig.LanguageResourcesCacheCollection, cacheModels, null,
            //    DateTime.Now.AddDays(3), Cache.NoSlidingExpiration, CacheItemPriority.AboveNormal, null);

            //resources =
            //    HttpContext.Current.Cache[CRRConfig.LanguageResourcesCacheCollection] as IEnumerable<LanguageDto>;
            //resources =
            //    (IEnumerable<LanguageDto>)HttpContext.Current.Cache[CRRConfig.LanguageResourcesCacheCollection];


            //HttpContext.Current.Cache.Insert(CRRConfig.LanguageResourcesCacheCollection, cacheModels, null, DateTime.Now.AddDays(3), Cache.NoSlidingExpiration);

            //resources =
            //    HttpContext.Current.Cache[CRRConfig.LanguageResourcesCacheCollection] as IEnumerable<LanguageDto>;
            //resources =
            //    (IEnumerable<LanguageDto>)HttpContext.Current.Cache[CRRConfig.LanguageResourcesCacheCollection];
        }

        public IEnumerable<LanguageDto> GetLanguageResources(string languageCode)
        {
            var resources = this.GetLanguageResources();

            if (resources == null)
            {
                return null;
            }

            resources = resources.Where(x => x.LanguageCode == languageCode);

            return resources;
        }

        public IEnumerable<LanguageDto> GetLanguageResources()
        {
            var resources =
                HttpContext.Current.Cache[CRRConfig.LanguageResourcesCacheCollection] as IEnumerable<LanguageDto>;

            if (resources == null)
            {
                this.LoadLanguageResources();

                resources =
                    HttpContext.Current.Cache[CRRConfig.LanguageResourcesCacheCollection] as IEnumerable<LanguageDto>;

                this.languageService.LogError("Language cache is empty.", nameof(CacheManager));

                return null;
            }

            return resources;
        }

        public string GetCssColor()
        {
            var color = HttpContext.Current.Session["css-forum-color"] as string;

            if (string.IsNullOrEmpty(color))
            {
                HttpContext.Current.Session["css-forum-color"] = "change";
                return "bg-info-c";
            }

            HttpContext.Current.Session["css-forum-color"] = null;
            return "bg-warning-c";
        }
    }
}