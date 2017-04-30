namespace CarRepairReport
{
    using CarRepairReport.Data;
    using CarRepairReport.Managers;
    using CarRepairReport.Services;

    public static class CacheConfig
    {
        public static void Config()
        {
            var cacheManager = new CacheManager(new LanguageService(new CarRepairReportData(ApplicationDbContext.Create())));

            cacheManager.LoadLanguageResources();
        }
    }
}