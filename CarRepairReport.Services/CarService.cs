namespace CarRepairReport.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using CarRepairReport.Data;
    using CarRepairReport.Models.Models.CarComponents;
    using CarRepairReport.Models.Models.CommonModels;
    using CarRepairReport.Services.Interfaces;

    public class CarService : Service, ICarService
    {
        public CarService(ICarRepairReportData context) : base(context)
        {
        }

        public bool AddCar(Car car, string appUserId)
        {
            var user = this.context.MyUsers.FirstOrDefault(x => x.ApplicationUserId == appUserId);

            if (user == null)
            {
                return false;
            }

            var entityEngine = this.context.Engines
                .FirstOrDefault(x =>
                    x.FuelType == car.Engine.FuelType &&
                    x.EnginePower == car.Engine.EnginePower &&
                    x.EngineSize == car.Engine.EngineSize);

            if (entityEngine != null)
            {
                car.Engine = entityEngine;
            }

            var entityGearbox = this.context.Gearboxes
                .FirstOrDefault(x =>
                    x.GearBoxType == car.Gearbox.GearBoxType &&
                    x.NumberOfGears == car.Gearbox.NumberOfGears);

            if (entityGearbox != null)
            {
                car.Gearbox = entityGearbox;
            }

            car.Owner = user;
            car.OwnerId = user.Id;
            user.Cars.Add(car);

            try
            {
                this.context.Commit();
            }
            catch (Exception ex)
            {
                return this.LogError(ex);
            }

            return true;
        }

        public Car GetById(int carId)
        {
            return this.context.Cars.FirstOrDefault(x => x.Id == carId && !x.IsDeleted);
        }

        public bool RemoveCar(string appUserId, int id)
        {
            var car = this.context.Cars.GetById(id);

            var isSameOwner = car.Owner.ApplicationUserId == appUserId;

            if (!isSameOwner)
            {
                return false;
            }


            try
            {
                this.context.Cars.Remove(car);

                this.context.Commit();
            }
            catch (Exception ex)
            {
                return this.LogError(ex);
            }


            return true;
        }

        public IEnumerable<Car> AllUserCars(string userId)
        {
            var cars = this.context.Cars.Where(x => x.Owner.ApplicationUserId == userId && !x.IsDeleted).ToArray();

            return cars;
        }

        public Manufacturer GetCarPartManufacturerByName(string manufacturerName)
        {
            return this.context.Manufacturers.FirstOrDefault(x => x.Name == manufacturerName);
        }

        public bool AddManufacturer(Manufacturer manufacturer)
        {
            try
            {
                this.context.Manufacturers.Add(manufacturer);
                this.context.Commit();
            }
            catch (Exception ex)
            {
                return this.LogError(ex);
            }

            return true;
        }

        public bool AddCarPart(CarPart newPart)
        {
            try
            {
                this.context.CarParts.Add(newPart);
                this.context.Commit();
            }
            catch (Exception ex)
            {
                return this.LogError(ex);
            }

            return true;
        }

        public bool AddInvestment(Cost investment)
        {
            try
            {
                this.context.Costs.Add(investment);
                this.context.Commit();
            }
            catch (Exception ex)
            {
                return this.LogError(ex);
            }

            return true;
        }

        public CarPart GetCarPartById(int id)
        {
            return this.context.CarParts.GetById(id);
        }

        public IEnumerable<CarPart> LatestCarParts(int count = 100)
        {
            return this.context.CarParts.All().OrderByDescending(x => x.CreatedOn).Take(count);
        }
    }
}
