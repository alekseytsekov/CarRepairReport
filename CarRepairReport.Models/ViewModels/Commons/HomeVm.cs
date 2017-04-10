namespace CarRepairReport.Models.ViewModels.Commons
{
    using CarRepairReport.Models.BindingModels;
    using CarRepairReport.Models.ViewModels.CarVms;

    public class HomeVm : ViewBindingModel
    {
        //public CreateInvestVm CreateCostVm { get; set; }

        //public CreateCarPartVm CreateCarPartVm { get; set; }

        public InvestPartBm InvestPart { get; set; }
    }
}
