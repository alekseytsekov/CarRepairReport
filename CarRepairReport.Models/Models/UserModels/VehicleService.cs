namespace CarRepairReport.Models.Models.UserModels
{
    using System.Collections.Generic;
    using CarRepairReport.Models.Models.AddressModels;
    using CarRepairReport.Models.Models.CommonModels;

    public class VehicleService : BaseModel
    {
        public VehicleService()
        {
            this.Costs = new HashSet<Cost>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public string LogoUrl { get; set; }
        public virtual ICollection<Cost> Costs { get; set; }
    }
}
