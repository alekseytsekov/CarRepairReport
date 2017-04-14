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
        public string Name { get; set; }
        public string Description { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public string LogoUrl { get; set; }
        public virtual ICollection<User> ServiceAdmins { get; set; }
        public virtual ICollection<CarPart> CarParts { get; set; }
    }
}
