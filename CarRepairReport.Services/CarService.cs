namespace CarRepairReport.Services
{
    using System.Security.Cryptography.X509Certificates;
    using CarRepairReport.Data;
    using CarRepairReport.Models.Models.CarComponents;
    using CarRepairReport.Services.Interfaces;

    public class CarService : Service , ICarService
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

            this.context.Commit();

            return true;
        }
    }
}
