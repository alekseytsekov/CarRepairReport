namespace CarRepairReport.Models.ViewModels.CarVms
{
    using System;

    public class RequestCarPartVm
    {
        public int Id { get; set; }

        public string PartName { get; set; }

        public string ManufacturerName { get; set; }

        public string CarDescription { get; set; }

        public string OwnerFullName { get; set; }

        public string Date { get; set; }
        
    }
}
