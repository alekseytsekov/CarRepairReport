namespace CarRepairReport.Services
{
    using System;
    using CarRepairReport.Data;
    using CarRepairReport.Models.Models.CommonModels;
    using CarRepairReport.Services.Interfaces;

    public class CommercialService : Service, ICommercialService
    {
        public CommercialService(ICarRepairReportData context) : base(context)
        {
        }

        public bool AddPromotion(Promotion promotion)
        {
            try
            {
                this.context.Promotions.Add(promotion);

                this.context.Commit();
            }
            catch (Exception ex)
            {
                return this.LogError(ex);
            }

            return true;
        }
    }
}
