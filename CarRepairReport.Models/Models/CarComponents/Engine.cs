namespace CarRepairReport.Models.Models.CarComponents
{
    using CarRepairReport.Models.Enums;

    public class Engine : BaseModel
    {
        public int  Id { get; set; }

        public FuelType FuelType { get; set; }

        public decimal EngineSize { get; set; }

        public int EnginePower { get; set; }
    }
}
