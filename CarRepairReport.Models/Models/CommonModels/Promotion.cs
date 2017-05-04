namespace CarRepairReport.Models.Models.CommonModels
{
    using CarRepairReport.Models.Enums;
    using CarRepairReport.Models.Models.UserModels;

    public class Promotion : BaseModel
    {
        public int Id { get; set; }
        
        public string Content { get; set; }

        public BelongTo Type { get; set; }

        public int VehicleServiceId { get; set; }

        public virtual VehicleService VehicleService { get; set; }

        public bool IsActive { get; set; }
    }
}
