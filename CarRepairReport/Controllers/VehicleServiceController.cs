namespace CarRepairReport.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using CarRepairReport.Globals;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.ViewModels.ServiceVms;

    public class VehicleServiceController : Controller
    {
        private IVehicleServiceManager vehicleServiceManager;

        public VehicleServiceController(IVehicleServiceManager vehicleServiceManager)
        {
            this.vehicleServiceManager = vehicleServiceManager;
        }
        
        [HttpGet]
        [Route("service/{id}")]
        public ActionResult VehicleService(int id)
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult TopVehicleServices()
        {
            ICollection<ShortServiceVm> vms = this.vehicleServiceManager
                .GetTopServicesShortInfo(Configurations.NumberOfTopVehicleServiceInHomeView);

            return this.PartialView("_TopVehicleServices", vms);
        }
    }
}