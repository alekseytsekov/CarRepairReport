namespace CarRepairReport.Models.Models.CarComponents
{
    public class CarPart : BaseModel
    {
        public int Id { get; set; }

        public string SerialNumber { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }

        public string Description { get; set; }
    }
}
