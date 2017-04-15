namespace CarRepairReport.Models.Models.UserModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using CarRepairReport.Models.Models.AddressModels;
    using CarRepairReport.Models.Models.CarComponents;
    using CarRepairReport.Models.Models.CommonModels;

    public class VehicleService : BaseModel
    {
        public VehicleService()
        {
            this.CarParts = new HashSet<CarPart>();
            this.ServiceAdmins = new HashSet<User>();
        }
        public int Id { get; set; }

        [Required]
        [MinLength(2), MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MinLength(2), MaxLength(1000)]
        public string Description { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public string LogoUrl { get; set; }

        [Required]
        [MinLength(2), MaxLength(200)]
        public string WorkingTime { get; set; }

        [Required]
        [MinLength(2), MaxLength(200)]
        public string WorkingDays { get; set; }

        [Required]
        [MinLength(2), MaxLength(200)]
        public string NonWorkingDays { get; set; }
        public virtual ICollection<User> ServiceAdmins { get; set; }
        public virtual ICollection<CarPart> CarParts { get; set; }
    }
}
