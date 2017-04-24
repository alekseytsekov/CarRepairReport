namespace CarRepairReport.Managers
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using CarRepairReport.Globals;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.Models.CarComponents;
    using CarRepairReport.Models.ViewModels.ManufacturerVms;
    using CarRepairReport.Services.Interfaces;

    public class ManufacturerManager : IManufacturerManager
    {
        private IManufacturerService manufacturerService;

        public ManufacturerManager(IManufacturerService manufacturerService)
        {
            this.manufacturerService = manufacturerService; 
        }

        public IEnumerable<ShortManufacturerInfoVm> GetTopManufacturersShortInfo(int take)
        {
            var manufacturers = this.manufacturerService
                .All()
                .OrderByDescending(x => x.CarParts.Count)
                .ThenBy(x => x.Name)
                .Take(take);

            var vms = Mapper.Map<IEnumerable<Manufacturer>, IEnumerable<ShortManufacturerInfoVm>>(manufacturers);

            return vms;
        }

        public ManufacturerVm GetManufacturerByName(string name)
        {
            var manufacturerEntity = this.manufacturerService.GetManufacturerByName(name);

            var vm = Mapper.Map<Manufacturer, ManufacturerVm>(manufacturerEntity);

            return vm;
        }
    }
}