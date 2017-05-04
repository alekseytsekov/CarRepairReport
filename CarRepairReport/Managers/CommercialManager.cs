namespace CarRepairReport.Managers
{
    using System.Linq;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.BindingModels.CommonBms;
    using CarRepairReport.Models.Enums;
    using CarRepairReport.Models.Models.CommonModels;
    using CarRepairReport.Services.Interfaces;

    public class CommercialManager : ICommercialManager
    {
        private IVehicleServiceService vehicleService;
        private ICommercialService commercialService;

        public CommercialManager(IVehicleServiceService vehicleService, ICommercialService commercialService)
        {
            this.vehicleService = vehicleService;
            this.commercialService = commercialService;
        }

        public bool CreatePromotion(PromotionBm bm, string getAppUserId)
        {
            var entityService = this.vehicleService.GetVehiceService(bm.Id);

            if (entityService == null)
            {
                return false;
            }

            var isMember = entityService.ServiceMembers.Any(x => x.ApplicationUserId == getAppUserId);

            if (!isMember)
            {
                return false;
            }

            foreach (var entityServicePromotion in entityService.Promotions)
            {
                entityServicePromotion.IsActive = false;
            }

            var promotion = new Promotion()
            {
                Type = BelongTo.VehicleService,
                Content = bm.Content,
                VehicleService = entityService,
                VehicleServiceId = entityService.Id,
                IsActive = true // just for now !!!
            };

            entityService.Promotions.Add(promotion);

            return this.commercialService.AddPromotion(promotion);
            
        }
    }
}