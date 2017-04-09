using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CarRepairReport
{
    using AutoMapper;
    using CarRepairReport.Models.BindingModels;
    using CarRepairReport.Models.Models;
    using CarRepairReport.Models.Models.UserModels;
    using CarRepairReport.Models.ViewModels;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //DependencyConfig.Register();
            this.ConfigureMapping();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void ConfigureMapping()
        {
            Mapper.Initialize(map =>
            {
                map.CreateMap<User, UserProfileVm>().ForMember(x=> x.Cars, y=> y.Ignore());
                map.CreateMap<EditUserBm, UserProfileBm>();
            });
        }
        
    }
}
