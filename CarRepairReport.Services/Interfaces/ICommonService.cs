namespace CarRepairReport.Services.Interfaces
{
    using System.Collections.Generic;
    using CarRepairReport.Models.Models.CommonModels;

    public interface ICommonService
    {
        IEnumerable<MembershipInvitation> GetInvitationsByEmail(string email);
    }
}
