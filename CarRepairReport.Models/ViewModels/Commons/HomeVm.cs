namespace CarRepairReport.Models.ViewModels.Commons
{
    using CarRepairReport.Models.ViewModels.CarVms;

    public class HomeVm : ViewBindingModel
    {
        public CreateCostVm CreateCostVm { get; set; }

        public CreateCarPartVm CreateCarPartVm { get; set; }
    }
}
