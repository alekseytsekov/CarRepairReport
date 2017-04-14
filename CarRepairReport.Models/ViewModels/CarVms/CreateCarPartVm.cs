namespace CarRepairReport.Models.ViewModels.CarVms
{
    using System.ComponentModel.DataAnnotations;

    public class CreateCarPartVm
    {
        public string SerialNumber { get; set; }
        
        public string PartName { get; set; }
        
        public decimal PartPrice { get; set; }
        
        public int Quantity { get; set; }

        public string ManufacturerName { get; set; }

        public int DistanceTraveled { get; set; }

        public int MountedOnKm { get; set; }

        public int MountedOnMi { get; set; }

        public string VehicleService { get; set; }
    }
}
