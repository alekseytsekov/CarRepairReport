namespace CarRepairReport.Managers.Interfaces
{
    using System.Collections.Generic;
    using CarRepairReport.Models.BindingModels.CommonBms;
    using CarRepairReport.Models.BindingModels.VehicleServiceBms;
    using CarRepairReport.Models.Dtos;
    using CarRepairReport.Models.ViewModels.CarVms;
    using CarRepairReport.Models.ViewModels.ServiceVms;

    public interface IVehicleServiceManager
    {
        ICollection<ShortServiceVm> GetTopServicesShortInfo(int take);
        VehicleServiceVm GetVm(int id, string appUserId);
        ResultDto SendInvitation(InviteMemberBm bm);
        IEnumerable<RequestCarPartVm> GetUnconfirmedParts(int serviceId);
        bool ProcessCarPart(string appUserId, AnswerBm bm);
        bool ProcessVote(AnswerBm bm, string appUserId);
        int GetRating(int serviceId);
    }
}
