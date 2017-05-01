namespace CarRepairReport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AutoMapper;
    using CarRepairReport.Extensions;
    using CarRepairReport.Models.BindingModels;
    using CarRepairReport.Models.Dtos;
    using CarRepairReport.Models.Models.CarComponents;
    using CarRepairReport.Models.Models.CommonModels;
    using CarRepairReport.Models.Models.ForumModels;
    using CarRepairReport.Models.Models.LanguageModels;
    using CarRepairReport.Models.Models.UserModels;
    using CarRepairReport.Models.ViewModels.CarVms;
    using CarRepairReport.Models.ViewModels.Commons;
    using CarRepairReport.Models.ViewModels.ForumVm;
    using CarRepairReport.Models.ViewModels.ManufacturerVms;
    using CarRepairReport.Models.ViewModels.ServiceVms;
    using CarRepairReport.Models.ViewModels.UserVms;

    public static class AutoMapperConfiguration
    {
        public static void Config()
        {
            Mapper.Initialize(map =>
            {
                map.CreateMap<User, UserProfileVm>().ForMember(x => x.Cars, y => y.Ignore());
                map.CreateMap<EditUserBm, UserProfileBm>();
                map.CreateMap<InvestPartBm, CreateInvestmentVm>();
                map.CreateMap<InvestPartBm, CreateCarPartVm>();

                map.CreateMap<VehicleService, ShortServiceVm>()
                .ForMember(x => x.Name, y => y.MapFrom(src => src.Name))
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
                    .ForMember(x => x.CarDescription, y => y.MapFrom(s => CreateCarDescription(s)))
                    .ForMember(x => x.ManufacturerName, y => y.MapFrom(s => s.Manufacturer.Name.ToCapital()))
                    .ForMember(x => x.OwnerFullName,
                        y => y.MapFrom(s => s.Car.Owner.FirstName + " " + s.Car.Owner.LastName))
                    .ForMember(x => x.PartName, y => y.MapFrom(s => s.Name.ToCapital()))
                    .ForMember(x => x.Date, y => y.MapFrom(s => s.CreatedOn.ToLocalTime().ToString("D")));

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
                    .ForMember(x => x.Description, y => y.MapFrom(s => s.Description));

                map.CreateMap<Cost, CostVm>()
                    .ForMember(x => x.Name, y => y.MapFrom(s => s.Name))
                    .ForMember(x => x.Price, y => y.MapFrom(s => s.Price))
                    .ForMember(x => x.RegisterOn, y => y.MapFrom(s => s.CreatedOn));

                map.CreateMap<Manufacturer, ManufacturerVm>()
                    .ForMember(x => x.Name, y => y.MapFrom(s => s.Name))
                    .ForMember(x => x.Parts, y => y.MapFrom(s => GroupCarPart(s)));

                map.CreateMap<LanguageValue, LanguageDto>()
                    .ForMember(x => x.LanguageCode, y => y.MapFrom(s => s.LangTwoLetterCode))
                    .ForMember(x => x.Key, y => y.MapFrom(s => s.Key))
                    .ForMember(x => x.Value, y => y.MapFrom(s => s.Value));

                map.CreateMap<CarPart, ServiceInfoVm>()
                    .ForMember(x => x.CarPartSerialNumber, y => y.MapFrom(s => s.SerialNumber))
                    .ForMember(x => x.CarPartName, y => y.MapFrom(s => s.Name))
                    .ForMember(x => x.CarPartManufacturerName, y => y.MapFrom(s => s.Manufacturer.Name))
                    .ForMember(x => x.ServicedBy, y => y.MapFrom(s => s.VehicleService.Name))
                    .ForMember(x => x.CarMake, y => y.MapFrom(s => s.Car.Make))
                    .ForMember(x => x.CarModel, y => y.MapFrom(s => s.Car.Model))
                    .ForMember(x => x.CarEngineType, y => y.MapFrom(s => s.Car.Engine.FuelType.ToString()))
                    .ForMember(x => x.CarYear, y => y.MapFrom(s => ToStringDate(s.Car.FirstRegistration)))
                    .ForMember(x => x.CarEngine, y => y.MapFrom(s => EngineToString(s.Car.Engine)))
                    .ForMember(x => x.CarGearbox, y => y.MapFrom(s => s.Car.Gearbox.GearBoxType.ToString()));

                map.CreateMap<Post, PostVm>()
                    .ForMember(x => x.Title, y => y.MapFrom(s => s.Title))
                    .ForMember(x => x.Content, y => y.MapFrom(s => s.Content))
                    .ForMember(x => x.AuthorName, y => y.MapFrom(s => s.Author.FirstName + " " + s.Author.LastName))
                    .ForMember(x => x.WebLink, y => y.MapFrom(s => s.WebLink()))
                    //.ForMember(x => x.Equals(), y => y.MapFrom(s => s.WebLink()))
                    .ForMember(x => x.Id, y => y.MapFrom(s => s.Id))
                    .ForMember(x => x.ParentId, y => y.MapFrom(s => s.ParentId))
                    .ForMember(x => x.CreatedOn, y => y.MapFrom(s => s.CreatedOn))
                    .ForMember(x => x.ModifiedOn, y => y.MapFrom(s => s.ModifiedOn))
                    .ForMember(x => x.Categories, y => y.MapFrom(s => s.Categories.Select(cat => cat.Name)))
                    .ForMember(x => x.Tags, y => y.MapFrom(s => s.Tags.Select(ta => ta.Name)));

                map.CreateMap<Post, ViewPostVm>()
                    .ForMember(x => x.Title, y => y.MapFrom(s => s.Title))
                    .ForMember(x => x.Content, y => y.MapFrom(s => s.Content))
                    .ForMember(x => x.AuthorName, y => y.MapFrom(s => s.Author.FirstName + " " + s.Author.LastName))
                    .ForMember(x => x.Original, y => y.MapFrom(s => s.Original))
                    .ForMember(x => x.Parent, y => y.MapFrom(s => s.Parent))
                    .ForMember(x => x.Children, y => y.MapFrom(s => s.Children))
                    .ForMember(x => x.WebLink, y => y.MapFrom(s => s.WebLink()))
                    .ForMember(x => x.Id, y => y.MapFrom(s => s.Id))
                    .ForMember(x => x.CreatedOn, y => y.MapFrom(s => s.CreatedOn))
                    .ForMember(x => x.ModifiedOn, y => y.MapFrom(s => s.ModifiedOn));
            });
        }

        //private static int GetOriginalId(Post post)
        //{
        //    var id = post.Id;

        //    int oId = 0;

        //    if (int.TryParse(post.OriginalId.ToString(), out oId))
        //    {
        //        return oId;
        //    }

        //    return id;
        //}

        private static string EngineToString(Engine carEngine)
        {
            return carEngine.EngineSize.ToString("F1") + ", " + carEngine.EnginePower;
        }

        private static string ToStringDate(DateTime carFirstRegistration)
        {
            return carFirstRegistration.ToString("D");
        }

        private static IDictionary<string, int> GroupCarPart(Manufacturer manufacturer)
        {
            var parts = new Dictionary<string, int>();

            parts = manufacturer.CarParts.GroupBy(x => x.Name).ToDictionary(y => y.Key, y => y.Count());

            return parts;
        }

        private static string CreateCarDescription(CarPart cp)
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