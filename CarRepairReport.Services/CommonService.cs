namespace CarRepairReport.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using CarRepairReport.Data;
    using CarRepairReport.Models.Models.CommonModels;
    using CarRepairReport.Services.Interfaces;
    public class CommonService : Service, ICommonService
    {
        public CommonService(ICarRepairReportData context) : base(context)
        {
        }

        public IEnumerable<MembershipInvitation> GetInvitationsByEmail(string email)
        {
            var invitations = this.context.MembershipInvitations.All().Where(x => x.MemberEmail == email && !x.IsDeleted && !x.IsAccepted);

            return invitations;
        }
    }
}
