namespace CarRepairReport.Models.Models.CarComponents
{
    using System.Collections.Generic;
    using CarRepairReport.Models.Enums;

    public class Engine : BaseModel
    {
        public Engine()
        {
            this.Cars = new HashSet<Car>();
        }

        public int  Id { get; set; }

        public FuelType FuelType { get; set; }

        public decimal EngineSize { get; set; }

        public int EnginePower { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
