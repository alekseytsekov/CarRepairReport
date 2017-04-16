namespace CarRepairReport.Models.ViewModels.ManufacturerVms
{
    public class ShortManufacturerInfoVm : ViewBindingModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfParts { get; set; }
    }
}
