namespace CarRepairReport.Models.Models.CommonModels
{
    using System.Collections.Generic;
    using CarRepairReport.Models.Models.CarComponents;
    using CarRepairReport.Models.Models.UserModels;

    // investment model
    public class Cost : BaseModel 
    {
        public Cost()
        {
            this.CarParts = new HashSet<CarPart>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int MountedOn { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int CarId { get; set; }

        public virtual Car Car { get; set; }

        public bool RequestedToVehicleService { get; set; }

        public bool IsApprovedByVehicleService { get; set; }

        public int VehicleServiceId { get; set; }

        public VehicleService VehicleService { get; set; }

        public ICollection<CarPart> CarParts { get; set; }
        
    }
}
