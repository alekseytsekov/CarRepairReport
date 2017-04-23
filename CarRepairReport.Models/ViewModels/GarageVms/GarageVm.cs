namespace CarRepairReport.Models.ViewModels.GarageVms
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using CarRepairReport.Models.ViewModels.CarVms;

    public class GarageVm : ViewBindingModel
    {
        public GarageVm()
        {
            this.AvailableCars = new Dictionary<int, string>();
        }

        [Display(Name = "Select a Car: ")]
        public int SelectedCar { get; set; }

        public FullCarVm Car { get; set; }
        public IDictionary<int, string> AvailableCars { get; set; }
    }
}
