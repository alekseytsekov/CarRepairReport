namespace CarRepairReport.Models.Models.CarComponents
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using CarRepairReport.Models.Models.UserModels;

    public class CarPart : BaseModel
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string SerialNumber { get; set; }

        [Required]
        [MinLength(1), MaxLength(100)]
        public string Name { get; set; }

        [Range(0d,100000d)]
        public decimal Price { get; set; }

        [Range(0, 4000000)]
        public int MountedOnKm { get; set; }

        public int ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public int CarId { get; set; }

        public virtual Car Car { get; set; }

        public string Description { get; set; }

        public bool RequestedToVehicleService { get; set; }

        public bool IsApprovedByVehicleService { get; set; }

        public bool IsSeenByVehicleService { get; set; }

        public int VehicleServiceId { get; set; }

        public VehicleService VehicleService { get; set; }
    }
}
