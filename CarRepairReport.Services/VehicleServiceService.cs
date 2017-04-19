namespace CarRepairReport.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarRepairReport.Data;
    using CarRepairReport.Models.Models.CommonModels;
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

        public VehicleService GetVehiceService(int id)
        {
            return this.context.VehicleServices.FirstOrDefault(x => x.Id == id);
        }

        public bool AddMembershipInvitation(MembershipInvitation membershipInvitation)
        {
            var isExist = this.context.AppUsers.Any(x => x.Email == membershipInvitation.MemberEmail);

            if (!isExist)
            {
                return false;
            }

            // check for spam ... if there is active invitation, service cannot send another one.
            var isDuplicate = this.context.MembershipInvitations.Any(x =>
                x.VehicleServiceId == membershipInvitation.VehicleServiceId &&
                x.VehicleServiceName == membershipInvitation.VehicleServiceName &&
                x.MemberEmail == membershipInvitation.MemberEmail &&
                !x.IsDeleted);

            var alreadyMember = this.context.MembershipInvitations.Any(x =>
                x.VehicleServiceId == membershipInvitation.VehicleServiceId &&
                x.VehicleServiceName == membershipInvitation.VehicleServiceName &&
                x.MemberEmail == membershipInvitation.MemberEmail &&
                x.IsAccepted);

            if (isDuplicate || alreadyMember)
            {
                return false;
            }

            try
            {
                this.context.MembershipInvitations.Add(membershipInvitation);
                this.context.Commit();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
