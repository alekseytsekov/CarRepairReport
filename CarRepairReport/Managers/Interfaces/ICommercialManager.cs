namespace CarRepairReport.Managers.Interfaces
{
    using CarRepairReport.Models.BindingModels.CommonBms;

    public interface ICommercialManager
    {
        bool CreatePromotion(PromotionBm bm, string getAppUserId);
    }
}
