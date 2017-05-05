namespace CarRepairReport.Models.ViewModels.CarVms
{
    using CarRepairReport.Models.Enums;

    public class SimpleCarVm : ViewBindingModel
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public FuelType FuelType { get; set; }
        public int RunningDistance { get; set; }
        public int NumberOfCosts { get; set; }
        public int NumberOfServices { get; set; }
        public decimal TotalSpent { get; set; }
    }
}
