namespace CarRepairReport.Models.ViewModels.ServiceVms
{
    using System.Collections.Generic;
    using CarRepairReport.Models.ViewModels.UserVms;

    public class MembersWrapperVm : ViewBindingModel
    {
        public IEnumerable<UserAsMemberVm> Members { get; set; }
    }
}
