namespace CarRepairReport.Managers.Interfaces
{
    using System.Collections.Generic;
    using CarRepairReport.Models.ViewModels.ManufacturerVms;

    public interface IManufacturerManager 
    {
        IEnumerable<ShortManufacturerInfoVm> GetTopManufacturersShortInfo(int take);
        ManufacturerVm GetManufacturerByName(string name);
    }
}
