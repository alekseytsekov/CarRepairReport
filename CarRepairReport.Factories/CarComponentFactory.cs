namespace CarRepairReport.Factories
{
    using System;
    using CarRepairReport.Models.Enums;
    using CarRepairReport.Models.Models.CarComponents;

    public class CarComponentFactory : ICarComponentFactory
    {
        public Engine GenerateEngine(FuelType fuelType, decimal engineSize, int enginePower)
        {
            var engine = new Engine()
            {
                FuelType = fuelType,
                EngineSize = engineSize,
                EnginePower = enginePower,
                CreatedOn = DateTime.UtcNow
            };

            return engine;
        }

        public Gearbox GenerateGearbox(GearBoxType gearBoxType, int numberOfGears)
        {
            var gearbox = new Gearbox()
            {
                GearBoxType = gearBoxType,
                NumberOfGears = numberOfGears,
                CreatedOn = DateTime.UtcNow
            };

            return gearbox;
        }
    }
}
