namespace CarRepairReport.Controllers
{
    using System.Web.Mvc;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.BindingModels.CommonBms;
    using CarRepairReport.Models.Dtos;
    using CarRepairReport.Models.ViewModels.Commons;

    public class CommercialController : BaseController
    {
        private ICommercialManager commercialManager;

        public CommercialController(ICommercialManager commercialManager, IMyUserManager myUserManager, ILanguageManager languageManager) : base(myUserManager, languageManager)
        {
            this.commercialManager = commercialManager;
        }

        [HttpGet]
        [Authorize]
        [Route("CreatePromotion/{serviceId}")]
        public ActionResult CreatePromotion(int serviceId)
        {

            if (serviceId < 1)
            {
                this.Response.StatusCode = 400;
                return this.View("_Custom400BadRequestError");
            }

            var vm = new PromotionVm();

            vm.LanguageCode = this.CurrentLanguageCode;
            vm.Id = serviceId;

            return this.View(vm);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Route("SavePromotion")]
        public JsonResult SavePromotion(PromotionBm bm)
        {
            if (bm.Id < 1)
            {
                this.Response.StatusCode = 400;
                return this.Json(new ResultDto("400"), JsonRequestBehavior.AllowGet);
            }

            ResultDto result = null;

            if (string.IsNullOrWhiteSpace(bm.Content))
            {

                result = new ResultDto("empty");

                return this.Json(result, JsonRequestBehavior.AllowGet);
            }

            bool isAdded = this.commercialManager.CreatePromotion(bm, this.GetAppUserId);

            if (!isAdded)
            {
                this.Response.StatusCode = 400;
                return this.Json(new ResultDto("400"), JsonRequestBehavior.AllowGet);
            }

            result = new ResultDto("success", true);

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}