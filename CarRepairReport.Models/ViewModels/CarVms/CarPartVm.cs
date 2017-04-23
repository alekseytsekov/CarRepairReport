namespace CarRepairReport.Models.ViewModels.CarVms
{
    using System;

    public class CarPartVm
    {
        public string SerialNumber { get; set; }
        
        public string Name { get; set; }
        
        public decimal Price { get; set; }
        
        public int MountedOn { get; set; }

        public string Manufacturer { get; set; }

        public string Description { get; set; }

        public bool RequestedToVehicleService { get; set; }

        public bool IsApprovedByVehicleService { get; set; }

        public bool IsSeenByVehicleService { get; set; }

        public string VehicleService { get; set; }

        public DateTime RegisterOn { get; set; }
    }
}
