namespace CarRepairReport.Managers.Interfaces
{
    using System.Collections.Generic;
    using CarRepairReport.Models.BindingModels.VehicleServiceBms;
    using CarRepairReport.Models.Dtos;
    using CarRepairReport.Models.ViewModels.ServiceVms;

    public interface IVehicleServiceManager
    {
        ICollection<ShortServiceVm> GetTopServicesShortInfo(int take);
        VehicleServiceVm GetVm(int id);
        ResultDto SendInvitation(InviteMemberBm bm);
    }
}
