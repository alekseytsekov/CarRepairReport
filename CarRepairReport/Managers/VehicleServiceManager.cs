namespace CarRepairReport.Managers
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.BindingModels.VehicleServiceBms;
    using CarRepairReport.Models.Dtos;
    using CarRepairReport.Models.Models.CommonModels;
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

        public VehicleServiceVm GetVm(int id)
        {
            var vService = this.vehicleService.GetVehiceService(id);

            if (vService == null)
            {
                return null;
            }

            var vm = Mapper.Map<VehicleService,VehicleServiceVm>(vService);

            var groupedBy = vm.CarParts.GroupBy(x => x.CarMake);

            foreach (var gr in groupedBy)
            {
                int a = gr.Count();
            }

            return vm;
        }

        public ResultDto SendInvitation(InviteMemberBm bm)
        {
            var membershipInvitation = new MembershipInvitation()
            {
                VehicleServiceId = bm.Id,
                MemberEmail = bm.MemberEmail
            };

            bool isAdded = this.vehicleService.AddMembershipInvitation(membershipInvitation);

            if (!isAdded)
            {
                return new ResultDto() {IsSucceed = false, Message = "Cannot add membership invitation!"};
            }

            return null;
        }
    }
}