namespace CarRepairReport.Models.Models.CommonModels
{
    using System.ComponentModel.DataAnnotations;
    using CarRepairReport.Models.Models.UserModels;

    public class ServiceRating : BaseModel
    {
        public int Id { get; set; }

        public bool IsPositive { get; set; }
        
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int VehicleServiceId { get; set; }

        public virtual VehicleService VehicleService { get; set; }

        [MaxLength(5000)]
        public string Message { get; set; }
        
    }
}
