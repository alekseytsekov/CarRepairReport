namespace CarRepairReport.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using CarRepairReport.Globals;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.ViewModels.ManufacturerVms;

    public class ManufacturerController : BaseController
    {
        private IManufacturerManager manufacturerManager;

        public ManufacturerController(IManufacturerManager manufacturerManager, IMyUserManager myUserManager) : base(myUserManager)
        {
            this.manufacturerManager = manufacturerManager;
        }
        
        [HttpGet]
        [ChildActionOnly]
        public ActionResult GetTopManufacturers()
        {
            var vms =
                this.manufacturerManager.GetTopManufacturersShortInfo(Configurations.NumberOfTopManufacturersInHomeView);

            return this.PartialView("ManufacturersShortInfo", vms);
        }

        [HttpGet]
        [Route("manufacturers/{id}")]
        public ActionResult Manufacturers(int id)
        {
            return null;
        }
    }
}