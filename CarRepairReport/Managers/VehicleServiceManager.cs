namespace CarRepairReport.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using AutoMapper;
    using CarRepairReport.Globals;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.BindingModels.CommonBms;
    using CarRepairReport.Models.BindingModels.VehicleServiceBms;
    using CarRepairReport.Models.Dtos;
    using CarRepairReport.Models.Models.CarComponents;
    using CarRepairReport.Models.Models.CommonModels;
    using CarRepairReport.Models.Models.UserModels;
    using CarRepairReport.Models.ViewModels.CarVms;
    using CarRepairReport.Models.ViewModels.ServiceVms;
    using CarRepairReport.Services.Interfaces;

    public class VehicleServiceManager : IVehicleServiceManager
    {
        private IVehicleServiceService vehicleService;
        private ICarService carService;
        private IUserService userService;

        public VehicleServiceManager(IVehicleServiceService vehicleService, ICarService carService, IUserService userService)
        {
            this.vehicleService = vehicleService;
            this.carService = carService;
            this.userService = userService;
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

        public VehicleServiceVm GetVm(int id, string appUserId)
        {
            var vService = this.vehicleService.GetVehiceService(id);

            if (vService == null)
            {
                return null;
            }

            var vm = Mapper.Map<VehicleService,VehicleServiceVm>(vService);

            vm.HasUserManagementRights = vService.ServiceMembers.Any(x => x.ApplicationUserId == appUserId);

            return vm;
        }

        public ResultDto SendInvitation(InviteMemberBm bm)
        {
            var vService = this.vehicleService.GetVehiceService(bm.Id);

            if (vService == null)
            {
                return new ResultDto("Cannot send membership invitation!");
            }

            var isSame = vService.ServiceMembers
                .FirstOrDefault(x => x.IsVehicleServiceOwner).ApplicationUser.Email == bm.MemberEmail;

            if (isSame)
            {
                return new ResultDto("Cannot send membership invitation!");
            }

            var membershipInvitation = new MembershipInvitation()
            {
                VehicleServiceId = bm.Id,
                VehicleServiceName = vService.Name,
                MemberEmail = bm.MemberEmail
            };

            bool isAdded = this.vehicleService.AddMembershipInvitation(membershipInvitation);

            if (!isAdded)
            {
                return new ResultDto("Cannot send membership invitation!");
            }

            return null;
        }

        public IEnumerable<RequestCarPartVm> GetUnconfirmedParts(int serviceId)
        {
            var vehicleServiceEntity = this.vehicleService.GetVehiceService(serviceId);

            if (vehicleServiceEntity == null && !vehicleServiceEntity.IsDeleted)
            {
                throw new ArgumentOutOfRangeException();
            }

            var carParts =
                vehicleServiceEntity.CarParts
                    .Where(
                        x =>
                            x.VehicleServiceId == vehicleServiceEntity.Id && !x.IsSeenByVehicleService &&
                            x.RequestedToVehicleService)
                    .OrderByDescending(x => x.CreatedOn);

            var vms = Mapper.Map<IEnumerable<CarPart>, IEnumerable<RequestCarPartVm>>(carParts);

            return vms;

        }

        public bool ProcessCarPart(string appUserId, AnswerBm bm)
        {
            var vehicleServiceEntity =
                this.vehicleService.GetAllVehicleServices()
                    .FirstOrDefault(x => x.ServiceMembers.Any(m => m.ApplicationUser.Id == appUserId));

            var carPartEntity = this.carService.GetCarPartById(bm.Id);

            if ( vehicleServiceEntity == null || carPartEntity == null || carPartEntity.VehicleServiceId != vehicleServiceEntity.Id)
            {
                return false;
            }

            if (bm.IsAccepted)
            {
                carPartEntity.IsSeenByVehicleService = true;
                carPartEntity.IsApprovedByVehicleService = true;
                carPartEntity.VehicleService = vehicleServiceEntity;
                carPartEntity.VehicleServiceId = vehicleServiceEntity.Id;

                vehicleServiceEntity.CarParts.Add(carPartEntity);
            }
            else
            {
                carPartEntity.IsSeenByVehicleService = true;
            }

            return this.carService.Update();
        }

        public bool ProcessVote(AnswerBm bm, string appUserId)
        {
            var vsEntity = this.vehicleService.GetVehiceService(bm.Id);

            if (vsEntity == null)
            {
                return false;
            }

            var serviceRating = vsEntity.ServiceRatings.FirstOrDefault(x => x.VehicleServiceId == vsEntity.Id && !x.IsDeleted && x.User.ApplicationUserId == appUserId);

            var user = this.userService.GetUserByAppId(appUserId);

            if (user == null)
            {
                return false;
            }

            if (serviceRating != null)
            {
                serviceRating.IsDeleted = true;
                var isUpdated = this.vehicleService.Update();

                if (!isUpdated)
                {
                    return false;
                }
            }

            var newServiceRating = new ServiceRating()
            {
                VehicleService = vsEntity,
                VehicleServiceId = vsEntity.Id,
                User = user,
                UserId = user.Id,
                IsPositive = bm.IsAccepted,
                Message = bm.Message
            };

            vsEntity.ServiceRatings.Add(newServiceRating);

            bool isAdded = this.vehicleService.AddServiceRating(newServiceRating);

            if (!isAdded)
            {
                return false;
            }

            return true;
        }

        public int GetRating(int serviceId)
        {
            return this.vehicleService.GetVehiceService(serviceId).GetRating();
        }

        public IEnumerable<VehicleServiceCommentVm> GetComments(int serviceId)
        {
            var vehicleService = this.vehicleService.GetVehiceService(serviceId);

            if (vehicleService == null)
            {
                return null;
            }

            var ratings = vehicleService.ServiceRatings.OrderByDescending(x => x.CreatedOn).Take(CRRConfig.VehicleServiceVotesOnPage);

            var comments = Mapper.Map<IEnumerable<ServiceRating>, IEnumerable<VehicleServiceCommentVm>>(ratings);

            return comments;
        }
    }
}