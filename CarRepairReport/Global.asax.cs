using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CarRepairReport
{
    using AutoMapper;
    using CarRepairReport.Models.BindingModels;
    using CarRepairReport.Models.Models;
    using CarRepairReport.Models.Models.CarComponents;
    using CarRepairReport.Models.Models.UserModels;
    using CarRepairReport.Models.ViewModels;
    using CarRepairReport.Models.ViewModels.CarVms;
    using CarRepairReport.Models.ViewModels.Commons;
    using CarRepairReport.Models.ViewModels.ManufacturerVms;
    using CarRepairReport.Models.ViewModels.ServiceVms;
    using CarRepairReport.Models.ViewModels.UserVms;

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
                map.CreateMap<InvestPartBm, CreateInvestmentVm>();
                map.CreateMap<InvestPartBm, CreateCarPartVm>();

                map.CreateMap<VehicleService, ShortServiceVm>()
                .ForMember(x=> x.Name, y=> y.MapFrom(src => src.Name))
                .ForMember(x => x.Rating, y => y.MapFrom(src => src.GetRating()))
                .ForMember(x => x.RepairedParts, y => y.MapFrom(src => src.CarParts.Count));

                map.CreateMap<Manufacturer, ShortManufacturerInfoVm>()
                    .ForMember(x => x.NumberOfParts, y => y.MapFrom(src => src.CarParts.Count));
            });
        }
        
    }
}
