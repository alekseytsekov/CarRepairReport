namespace CarRepairReport.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
                this.manufacturerManager.GetTopManufacturersShortInfo(CRRConfig.NumberOfTopManufacturersInHomeView);

            return this.PartialView("ManufacturersShortInfo", vms);
        }

        [HttpGet]
        [Route("manufacturer/{name}")]
        [HandleError(ExceptionType = typeof(ArgumentOutOfRangeException), View = "BadRequestError")]
        public ActionResult Manufacturer(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                this.Response.StatusCode = 404;
                return this.View("_Custom404FileNotFound");
            }
            
            ManufacturerVm vm = this.manufacturerManager.GetManufacturerByName(name.ToLower());

            if (vm == null)
            {
                this.Response.StatusCode = 404;
                return this.View("_Custom404FileNotFound");
            }
            
            return this.View(vm);
        }

        [HttpGet]
        [Route("manufacturers")]
        public ActionResult Manufacturers()
        {
            var vms = this.manufacturerManager.GetTopManufacturersShortInfo(int.MaxValue)
                .OrderBy(x => x.Name)
                .ThenByDescending(x => x.NumberOfParts);

            return this.View(vms);
        }
    }
}