namespace CarRepairReport.Models
{
    using System.Collections.Generic;
    using CarRepairReport.Models.Dtos;
    using CarRepairReport.Models.ViewModels.Commons;

    public abstract class ViewBindingModel
    {
        public ViewBindingModel()
        {
            this.Invitations = new List<MembershipInvitationVm>();
        }

        public string LanguageCode { get; set; }

        public ResultDto Result { get; set; }

        public ICollection<MembershipInvitationVm> Invitations { get; set; }
    }
}
