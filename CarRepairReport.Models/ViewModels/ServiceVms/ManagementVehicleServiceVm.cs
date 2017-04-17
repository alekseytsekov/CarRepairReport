namespace CarRepairReport.Models.ViewModels.ServiceVms
{
    using System.Collections.Generic;
    using CarRepairReport.Models.ViewModels.CarVms;

    public class ManagementVehicleServiceVm : ViewBindingModel
    {
        public ManagementVehicleServiceVm()
        {
            //this.RequestedCarParts = new List<RequestCarPartVm>();
        }

        public int Id { get; set; }

        public string InvitationEmail { get; set; }

        //public ICollection<RequestCarPartVm> RequestedCarParts { get; set; }

        // editable props
    }
}
