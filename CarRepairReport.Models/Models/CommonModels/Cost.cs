namespace CarRepairReport.Models.Models.CommonModels
{
    using CarRepairReport.Models.Models.CarComponents;
    using CarRepairReport.Models.Models.UserModels;

    public class Cost : BaseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int MountedOn { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int CarId { get; set; }

        public virtual Car Car { get; set; }

        public int CarPartId { get; set; }

        public virtual CarPart CarPart { get; set; }
        
    }
}
