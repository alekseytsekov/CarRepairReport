namespace CarRepairReport.Services.Interfaces
{
    using System.Collections.Generic;
    using CarRepairReport.Models.Models.CommonModels;

    public interface ICommonService : IService
    {
        IEnumerable<MembershipInvitation> GetInvitationsByEmail(string email);
        MembershipInvitation GetMembershipInvitationById(int id);
    }
}
