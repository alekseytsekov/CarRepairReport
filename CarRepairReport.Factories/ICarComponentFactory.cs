namespace CarRepairReport.Factories
{
    using CarRepairReport.Models.Enums;
    using CarRepairReport.Models.Models.CarComponents;

    public interface ICarComponentFactory
    {
        Engine GenerateEngine(FuelType bmFuelType, decimal bmEngineSize, int enginePower);
    }
}
