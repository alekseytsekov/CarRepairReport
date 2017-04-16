namespace CarRepairReport.Managers.Interfaces
{
    using System.Collections.Generic;
    using CarRepairReport.Models.ViewModels.ServiceVms;

    public interface IVehicleServiceManager
    {
        ICollection<ShortServiceVm> GetTopServicesShortInfo(int take);
    }
}
