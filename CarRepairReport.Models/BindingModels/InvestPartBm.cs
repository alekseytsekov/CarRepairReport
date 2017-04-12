namespace CarRepairReport.Models.BindingModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class InvestPartBm
    {
        public InvestPartBm()
        {
            this.VehicleServices = new List<string>();
            this.CarNames = new Dictionary<int, string>();
        }

        public string Name { get; set; }
        
        public decimal Price { get; set; }

        public int DistanceTraveled { get; set; }

        public int MountedOnKm { get; set; }

        public int MountedOnMi { get; set; }

        public string SerialNumber { get; set; }
        
        public string PartName { get; set; }
        
        public decimal PartPrice { get; set; }
        
        public int Quantity { get; set; }

        public string ManufacturerName { get; set; }

        [Display( Name = "Serviced by:")]
        public string VehicleService { get; set; }
        public ICollection<string> VehicleServices { get; set; }

        [Display(Name = "Choose a car:")]
        public int CarId { get; set; }
        public IDictionary<int,string> CarNames { get; set; }
    }
}
