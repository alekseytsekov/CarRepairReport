namespace CarRepairReport.Models.ViewModels.ForumVm
{
    using System.Collections.Generic;
    using CarRepairReport.Models.ViewModels.Commons;

    public class CategoryTagVm :ViewBindingModel
    {
        public IEnumerable<TagVm> Tags { get; set; }
        public IEnumerable<CategoryVm> Categories { get; set; }

    }
}
