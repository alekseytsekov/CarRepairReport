namespace CarRepairReport.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using CarRepairReport.Globals;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.BindingModels.VehicleServiceBms;
    using CarRepairReport.Models.Dtos;
    using CarRepairReport.Models.ViewModels.ServiceVms;

    public class VehicleServiceController : BaseController
    {
        private IVehicleServiceManager vehicleServiceManager;

        public VehicleServiceController(IVehicleServiceManager vehicleServiceManager, IMyUserManager myUserManager) : base(myUserManager)
        {
            this.vehicleServiceManager = vehicleServiceManager;
        }
        
        [HttpGet]
        [Route("service/{id}")]
        public ActionResult VehicleService(int id)
        {
            VehicleServiceVm vm = this.vehicleServiceManager.GetVm(id);

            if (vm == null)
            {
                // error page not found
            }

            vm.WorkingTime = string.Format(vm.WorkingTime, "From", "To");

            return this.View(vm);
        }

        [ChildActionOnly]
        public ActionResult TopVehicleServices()
        {
            ICollection<ShortServiceVm> vms = this.vehicleServiceManager
                .GetTopServicesShortInfo(Configurations.NumberOfTopVehicleServiceInHomeView);

            return this.PartialView("_TopVehicleServices", vms);
        }

        [HttpGet]
        //[Authorize(Roles = "service-member,service-owner")]
        [Route("service/{id}/manage")]
        public ActionResult Manage(int id)
        {
            var vm = new ManagementVehicleServiceVm();
            vm.Id = 4;

            return this.View(vm);
        }

        [HttpPost]
        //[Authorize(Roles = "service-member,service-owner")]
        [Route("service/{id}/manage")]
        public ActionResult Manage()
        {
            return this.RedirectToAction("Manage", 4);
        }

        [HttpGet]
        //[Authorize(Roles = "service-member,service-owner")]
        [Route("members/{serviceId}")]
        public ActionResult Members(int serviceId)
        {
            var vm = new InviteMemberVm() {Id = serviceId};

            return this.PartialView(vm);
        }

        [HttpPost]
        //[Authorize(Roles = "service-member,service-owner")]
        [Route("invite/{serviceId}")]
        public ActionResult Invite(InviteMemberBm bm)
        {
            if (!this.ModelState.IsValid)
            {
                return new JsonResult() { Data = new ResultDto() {Message = "Invalid email format!"} };
            }

            // membership invitation 
            ResultDto result = this.vehicleServiceManager.SendInvitation(bm);

            if (result != null)
            {
                return new JsonResult() { Data = result };
            }

            return new JsonResult() {Data = new ResultDto() {IsSucceed = true} };
        }
    }
}