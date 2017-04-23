﻿using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CarRepairReport
{
    using System.Linq;
    using System.Text;
    using AutoMapper;
    using CarRepairReport.Extensions;
    using CarRepairReport.Models.BindingModels;
    using CarRepairReport.Models.Models;
    using CarRepairReport.Models.Models.CarComponents;
    using CarRepairReport.Models.Models.CommonModels;
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
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = new System.Globalization.CultureInfo("en-US");

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

                map.CreateMap<CarPart, BasicCarPartVm>()
                    .ForMember(x => x.Name, y => y.MapFrom(s => s.Name))
                    .ForMember(x => x.CarMake, y => y.MapFrom(s => s.Car.Make))
                    .ForMember(x => x.CarModel, y => y.MapFrom(s => s.Car.Model));

                map.CreateMap<CarPart, RequestCarPartVm>()
                    .ForMember(x => x.Id, y => y.MapFrom(s => s.Id))
                    .ForMember(x => x.CarDescription, y => y.MapFrom(s => this.CreateCarDescription(s)))
                    .ForMember(x => x.ManufacturerName, y => y.MapFrom(s => s.Manufacturer.Name.ToCapital()))
                    .ForMember(x => x.OwnerFullName,
                        y => y.MapFrom(s => s.Car.Owner.FirstName + " " + s.Car.Owner.LastName))
                    .ForMember(x => x.PartName, y => y.MapFrom(s => s.Name.ToCapital()))
                    .ForMember(x => x.Date, y=> y.MapFrom(s => s.CreatedOn.ToLocalTime().ToString("D")));

                map.CreateMap<VehicleService, VehicleServiceVm>()
                    .ForMember(x => x.Name, y => y.MapFrom(s => s.Name))
                    .ForMember(x => x.Description, y => y.MapFrom(s => s.Description))
                    .ForMember(x => x.CityName, y => y.MapFrom(s => s.Address.City.Name))
                    .ForMember(x => x.StreetName, y => y.MapFrom(s => s.Address.StreetName))
                    .ForMember(x => x.CountryName, y => y.MapFrom(s => s.Address.City.Country.Name))
                    .ForMember(x => x.WorkingDays, y => y.MapFrom(s => s.WorkingDays))
                    .ForMember(x => x.NonWorkingDays, y => y.MapFrom(s => s.NonWorkingDays))
                    .ForMember(x => x.WorkingTime, y => y.MapFrom(s => s.WorkingTime))
                    .ForMember(x => x.Rating, y => y.MapFrom(s => s.GetRating()))
                    .ForMember(x => x.CarParts, y => y.MapFrom(s => s.CarParts.Where(x => x.IsApprovedByVehicleService && x.IsSeenByVehicleService && !x.IsDeleted)))
                    .ForMember(x => x.LogoUrl, y => y.MapFrom(s => s.LogoUrl));

                map.CreateMap<MembershipInvitation, MembershipInvitationVm>()
                    .ForMember(x => x.Name, y => y.MapFrom(s => s.VehicleServiceName))
                    .ForMember(x => x.SenderId, y => y.MapFrom(s => s.VehicleServiceId));

                map.CreateMap<ServiceRating, VehicleServiceCommentVm>()
                    .ForMember(x => x.Author, y => y.MapFrom(s => s.User.FirstName + " " + s.User.LastName))
                    .ForMember(x => x.Date, y => y.MapFrom(s => s.CreatedOn.ToString("d")))
                    .ForMember(x => x.Comment, y => y.MapFrom(s => s.Message))
                    .ForMember(x => x.IsPositive, y => y.MapFrom(s => s.IsPositive));

                map.CreateMap<Car, FullCarVm>()
                    .ForMember(x => x.CarNickname, y => y.MapFrom(s => s.CarNickname))
                    .ForMember(x => x.Make, y => y.MapFrom(s => s.Make))
                    .ForMember(x => x.Model, y => y.MapFrom(s => s.Model))
                    .ForMember(x => x.VIN, y => y.MapFrom(s => s.VIN))
                    .ForMember(x => x.FirstRegistration, y => y.MapFrom(s => s.FirstRegistration))
                    .ForMember(x => x.RunningDistance, y => y.MapFrom(s => s.RunningDistance))
                    .ForMember(x => x.SpendOnCosts, y => y.MapFrom(s => s.SpendOnCosts()))
                    .ForMember(x => x.SpendOnCarParts, y => y.MapFrom(s => s.SpendOnCarParts()))
                    .ForMember(x => x.TotalSpendOnCar, y => y.MapFrom(s => s.TotalSpendOnCar()))
                    .ForMember(x => x.Engine,
                        y =>
                            y.MapFrom(
                                s => s.Engine.EnginePower + ", " + s.Engine.EngineSize.ToString("F1") + ", " + s.Engine.FuelType))
                    .ForMember(x => x.Gearbox, y => y.MapFrom(s => s.Gearbox.GearBoxType + ", " + s.Gearbox.NumberOfGears));

                map.CreateMap<CarPart, CarPartVm>()
                    .ForMember(x => x.Name, y => y.MapFrom(s => s.Name))
                    .ForMember(x => x.SerialNumber, y => y.MapFrom(s => s.SerialNumber))
                    .ForMember(x => x.RegisterOn, y => y.MapFrom(s => s.CreatedOn))
                    .ForMember(x => x.Price, y => y.MapFrom(s => s.Price))
                    .ForMember(x => x.MountedOn, y => y.MapFrom(s => s.MountedOnKm))
                    .ForMember(x => x.Manufacturer, y => y.MapFrom(s => s.Manufacturer.Name))
                    .ForMember(x => x.VehicleService, y => y.MapFrom(s => s.VehicleService.Name))
                    .ForMember(x => x.RequestedToVehicleService, y => y.MapFrom(s => s.RequestedToVehicleService))
                    .ForMember(x => x.IsSeenByVehicleService, y => y.MapFrom(s => s.IsSeenByVehicleService))
                    .ForMember(x => x.IsApprovedByVehicleService, y => y.MapFrom(s => s.IsApprovedByVehicleService))
                    .ForMember(x => x.Description, y=> y.MapFrom(s => s.Description));

                map.CreateMap<Cost, CostVm>()
                    .ForMember(x => x.Name, y => y.MapFrom(s => s.Name))
                    .ForMember(x => x.Price, y => y.MapFrom(s => s.Price))
                    .ForMember(x => x.RegisterOn, y => y.MapFrom(s => s.CreatedOn));

            });
        }

        private string CreateCarDescription(CarPart cp)
        {
            var car = cp.Car;
            var description = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(car.CarNickname))
            {
                description.Append(car.CarNickname.ToCapital() + ", ");
            }

            description.Append(car.Model.ToCapital() + ", ");
            description.Append(car.Make.ToCapital() + ", ");
            description.Append(car.FirstRegistration.Year);

            return description.ToString();
        }
    }
}
