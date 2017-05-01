namespace CarRepairReport.Models.ViewModels.ForumVm
{
    using System.Collections.Generic;

    public class PostWrapperVm : ViewBindingModel
    {
        public int Page { get; set; }

        public IEnumerable<PostVm> Posts { get; set; }
    }
}
