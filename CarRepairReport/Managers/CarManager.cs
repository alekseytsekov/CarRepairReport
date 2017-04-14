namespace CarRepairReport.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.BindingModels;
    using CarRepairReport.Models.Models.CarComponents;
    using CarRepairReport.Models.Models.CommonModels;
    using CarRepairReport.Models.Models.UserModels;
    using CarRepairReport.Models.ViewModels.CarVms;
    using CarRepairReport.Models.ViewModels.Commons;
    using CarRepairReport.Services.Interfaces;

    public class CarManager : ICarManager
    {
        private const double OneKw = 1.34d;
        private const double OneMile = 1.609d;

        private ICarService carService;
        private IVehicleServiceService vehicleService;
        private IAddressService addressService;

        public CarManager(ICarService carService, IVehicleServiceService vehicleService, IAddressService addressService)
        {
            this.carService = carService;
            this.vehicleService = vehicleService;
            this.addressService = addressService;
        }

        public bool CreateCar(CreateCarBm bm, string appUserId)
        {
            int enginePower = this.EnginePowerToHp(bm.EnginePowerHp, bm.EnginePowerKw);

            if (enginePower == 0)
            {
                return false;
            }

            int runningDistance = this.RunningDistanceToKm(bm.RunningDistanceKm, bm.RunningDistanceM);

            if (runningDistance == 0)
            {
                return false;
            }
            
            Engine engine = new Engine()
            {
                FuelType = bm.FuelType,
                EngineSize = bm.EngineSize,
                EnginePower = enginePower
            };
            
            Gearbox gearbox = new Gearbox()
            {
                GearBoxType = bm.GearBoxType,
                NumberOfGears = bm.NumberOfGears
            };

            Car car = new Car()
            {
                Make = bm.Make,
                Model = bm.Model,
                FirstRegistration = bm.FirstRegistration,
                VIN = bm.VIN,
                RunningDistance = runningDistance,
                Engine = engine,
                Gearbox = gearbox
            };

            bool isAdded = this.carService.AddCar(car, appUserId);

            return true;
        }

        public SimpleCarVm MapToSimpleVm(Car car)
        {
            var vmCar = new SimpleCarVm()
            {
                Id = car.Id,
                Model = car.Model,
                Make = car.Make,
                FuelType = car.Engine.FuelType,
                RunningDistance = car.RunningDistance,
                NumberOfServices = car.CarParts.Count,
                NumberOfCosts = car.Costs.Count,
                TotalSpent = car.TotalSpendOnCar()
            };

            return vmCar;
        }

        public SimpleCarVm GetSimpleVm(string appUserId, int carId)
        {
            var car = this.carService.GetById(carId);

            if (car == null)
            {
                return null;
            }

            var carBelongToUser = car.Owner.ApplicationUserId == appUserId;

            if (!carBelongToUser)
            {
                return null;
            }

            return this.MapToSimpleVm(car);
        }

        public bool RemoveCarFromUser(string appUserId, int id)
        {
            bool isRemoved = this.carService.RemoveCar(appUserId, id);

            return isRemoved;
        }

        public IEnumerable<string> GetVehicleServiceNames()
        {
            var serviceNames = this.vehicleService.GetAllVehicleServices().Select(x => x.Name);

            return serviceNames;
        }

        public IDictionary<int,string> GetCarNames(string userId)
        {
            var cars = this.carService.AllUserCars(userId);

            var carNames = new Dictionary<int, string>();

            carNames.Add(0, string.Empty);

            foreach (var car in cars)
            {
                carNames.Add(car.Id, car.ToString());
            }

            return carNames;
        }

        public bool AddReplacedPart(CreateCarPartVm carPart, int carId, string appUserId)
        {
            var entityCar = this.carService.GetById(carId);

            if (entityCar.Owner.ApplicationUserId != appUserId)
            {
                return false;
            }

            var entityManufacturer = this.carService.GetCarPartManufacturerByName(carPart.ManufacturerName.ToLower());

            if (entityManufacturer == null)
            {
                entityManufacturer = new Manufacturer()
                {
                    Name = carPart.ManufacturerName.ToLower()
                };

                var isManufacturerAdded = this.carService.AddManufacturer(entityManufacturer);

                if (!isManufacturerAdded)
                {
                    return false;
                }
            }

            // change car running distance .... 
            var runningDistanceInKm = this.RunningDistanceToKm(carPart.MountedOnKm, carPart.MountedOnMi);

            if (runningDistanceInKm > 0 && entityCar.RunningDistance < runningDistanceInKm)
            {
                entityCar.RunningDistance = runningDistanceInKm;
            }
            else if (carPart.DistanceTraveled > 0)
            {
                entityCar.RunningDistance += carPart.DistanceTraveled;
            }
            
            // create new part 
            var newPart = new CarPart()
            {
                CreatedOn = DateTime.UtcNow,
                SerialNumber = carPart.SerialNumber.ToUpper(),
                Price = carPart.PartPrice,
                Name = carPart.PartName.ToLower(),
                ManufacturerId = entityManufacturer.Id,
                MountedOnKm = entityCar.RunningDistance,
                Car = entityCar,
                CarId = entityCar.Id
            };
            
            // create/register vehicle service
            if (carPart.VehicleService.ToLower() == "by me")
            {
                var serviceName = carPart.VehicleService.ToLower();
                var vehicleService = this.vehicleService.GetAllVehicleServices().FirstOrDefault(x => x.Name == serviceName);

                if (vehicleService == null)
                {
                    vehicleService = new VehicleService()
                    {
                        Name = "by me",
                        CreatedOn = DateTime.UtcNow
                    };

                    var address = this.addressService.GetAllAddresses().FirstOrDefault(x => x.StreetName == "My Street");

                    vehicleService.Address = address;
                    vehicleService.AddressId = address.Id;

                    bool isAdded = this.vehicleService.AddVehicleService(vehicleService);

                    if (!isAdded)
                    {
                        return false;
                    }
                }
                else
                {
                    newPart.VehicleService = vehicleService;
                    newPart.VehicleServiceId = vehicleService.Id;
                    vehicleService.CarParts.Add(newPart);
                }
            }
            else
            {
                var serviceName = carPart.VehicleService.ToLower();
                var vehicleService = this.vehicleService.GetAllVehicleServices().FirstOrDefault(x => x.Name == serviceName);

                if (vehicleService != null)
                {
                    newPart.VehicleService = vehicleService;
                    newPart.VehicleServiceId = vehicleService.Id;
                    vehicleService.CarParts.Add(newPart);
                    newPart.RequestedToVehicleService = true;
                }
            }
            
            entityManufacturer.CarParts.Add(newPart);
            entityCar.CarParts.Add(newPart);

            bool isCarPartAdded = this.carService.AddCarPart(newPart);

            return isCarPartAdded;
        }

        public Cost AddNewInvestment(CreateInvestmentVm investment, int carId, string appUserId)
        {
            var entityCar = this.carService.GetById(carId);

            if (entityCar.Owner.ApplicationUserId != appUserId)
            {
                return null;
            }

            var newInvestment = new Cost()
            {
                Name = investment.Name,
                Price = investment.Price,
                Car = entityCar,
                CarId = entityCar.Id,
                User = entityCar.Owner,
                UserId = entityCar.OwnerId,
                CreatedOn = DateTime.UtcNow,
                
            };

            bool isInvestmentAdded = this.carService.AddInvestment(newInvestment);

            if (!isInvestmentAdded)
            {
                return null;
            }

            return newInvestment;
            
        }

        private int RunningDistanceToKm(int kms, int miles)
        {
            if (kms == 0 && miles == 0)
            {
                return 0;
            }

            if (kms == 0)
            {
                return (int)(OneMile * miles);
            }

            return kms;
        }

        private int EnginePowerToHp(int hp, int kw)
        {
            if (hp == 0 && kw == 0)
            {
                return 0;
            }

            if (hp == 0)
            {
                return (int) (OneKw * kw);
            }

            return hp;
        }
    }
}