﻿namespace CarRepairReport.Managers
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.Models.UserModels;
    using CarRepairReport.Models.ViewModels.ServiceVms;
    using CarRepairReport.Services.Interfaces;

    public class VehicleServiceManager : IVehicleServiceManager
    {
        private IVehicleServiceService vehicleService;

        public VehicleServiceManager(IVehicleServiceService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        public ICollection<ShortServiceVm> GetTopServicesShortInfo(int take)
        {
            var services = this.vehicleService
                .GetAllVehicleServices()
                .OrderByDescending(x => x.CarParts.Count)
                .ThenByDescending(x => x.GetRating())
                .Take(take).ToArray();

            var vms = Mapper.Map<IEnumerable<VehicleService>,IEnumerable<ShortServiceVm>>(services);

            return vms.ToList();
        }
    }
}