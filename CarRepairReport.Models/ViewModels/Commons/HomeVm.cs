namespace CarRepairReport.Models.ViewModels.Commons
{
    using System.Collections.Generic;
    using CarRepairReport.Models.BindingModels;
    using CarRepairReport.Models.ViewModels.CarVms;
    using CarRepairReport.Models.ViewModels.ServiceVms;

    public class HomeVm : ViewBindingModel
    {
        public HomeVm()
        {
            //this.ShortServices = new List<ShortServiceVm>();
        }
        //public CreateInvestVm CreateCostVm { get; set; }

        //public CreateCarPartVm CreateCarPartVm { get; set; }

        public InvestPartBm InvestPart { get; set; }

        //public ICollection<ShortServiceVm> ShortServices { get; set; }
    }
}
