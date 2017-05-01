using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CarRepairReport
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = new System.Globalization.CultureInfo("en-US");

            //DependencyConfig.Register();
            AutoMapperConfiguration.Config();
            CacheConfig.Config();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        
    }
}
