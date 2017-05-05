namespace CarRepairReport.Models.ViewModels.ManufacturerVms
{
    using System.Collections.Generic;

    public class ManufacturerVm : ViewBindingModel
    {
        public string Name { get; set; }

        public IDictionary<string,int> Parts { get; set; }
    }
}
