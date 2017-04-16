namespace CarRepairReport.Services.Interfaces
{
    using System.Collections.Generic;
    using CarRepairReport.Models.Models.CarComponents;

    public interface IManufacturerService
    {
        IEnumerable<Manufacturer> All();
    }
}
