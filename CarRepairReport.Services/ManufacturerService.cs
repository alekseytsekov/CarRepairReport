namespace CarRepairReport.Services
{
    using System.Collections.Generic;
    using CarRepairReport.Data;
    using CarRepairReport.Models.Models.CarComponents;
    using CarRepairReport.Services.Interfaces;
    public class ManufacturerService : Service, IManufacturerService
    {
        public ManufacturerService(ICarRepairReportData context) : base(context)
        {
        }

        public IEnumerable<Manufacturer> All()
        {
            return this.context.Manufacturers.All();
        }

        public Manufacturer GetManufacturerByName(string name)
        {
            return this.context.Manufacturers.FirstOrDefault(x => x.Name == name);
        }
    }
}
