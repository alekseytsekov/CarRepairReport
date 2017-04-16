namespace CarRepairReport.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarRepairReport.Data;
    using CarRepairReport.Models.Models.UserModels;
    using CarRepairReport.Services.Interfaces;

    public class VehicleServiceService : Service, IVehicleServiceService
    {
        public VehicleServiceService(ICarRepairReportData context) : base(context)
        {
        }

        public IEnumerable<VehicleService> GetAllVehicleServices()
        {
            var vehicleServices = this.context.VehicleServices.All();
            return vehicleServices;
        }

        public bool AddVehicleService(VehicleService vehicleService)
        {
            try
            {
                this.context.VehicleServices.Add(vehicleService);
                this.context.Commit();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool IsServiceNameUnique(string serviceName)
        {
            return !this.context.VehicleServices.Any(x => x.Name == serviceName);
        }
    }
}
