namespace CarRepairReport.Services.Interfaces
{
    using System.Collections.Generic;
    using CarRepairReport.Models.Models.CommonModels;
    using CarRepairReport.Models.Models.UserModels;

    public interface IVehicleServiceService : IService
    {
        IEnumerable<VehicleService> GetAllVehicleServices();
        bool AddVehicleService(VehicleService vehicleService);
        bool IsServiceNameUnique(string serviceName);
        VehicleService GetVehiceService(int id);
        bool AddMembershipInvitation(MembershipInvitation membershipInvitation);
        bool AddServiceRating(ServiceRating serviceRating);
    }
}
