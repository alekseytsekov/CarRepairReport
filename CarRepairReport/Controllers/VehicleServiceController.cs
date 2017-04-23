namespace CarRepairReport.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using CarRepairReport.Globals;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.BindingModels.CommonBms;
    using CarRepairReport.Models.BindingModels.VehicleServiceBms;
    using CarRepairReport.Models.Dtos;
    using CarRepairReport.Models.ViewModels.CarVms;
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
            VehicleServiceVm vm = this.vehicleServiceManager.GetVm(id, this.GetAppUserId());

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

        [System.Web.Mvc.HttpGet]
        //[Authorize(Roles = "service-member,service-owner")]
        [Route("service/{id}/manage")]
        public ActionResult Manage(int id)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            var vm = new ManagementVehicleServiceVm();
            vm.Id = id;

            return this.View(vm);
        }

        [System.Web.Mvc.HttpPost]
        //[Authorize(Roles = "service-member,service-owner")]
        [Route("service/{id}/manage")]
        public ActionResult Manage()
        {
            return this.RedirectToAction("Manage", 4);
        }

        [System.Web.Mvc.HttpGet]
        //[Authorize(Roles = "service-member,service-owner")]
        [Route("members/{serviceId}")]
        
        public ActionResult Members(int serviceId)
        {
            var vm = new InviteMemberVm() {Id = serviceId};

            return this.PartialView(vm);
        }

        [System.Web.Mvc.HttpPost]
        //[Authorize(Roles = "service-member,service-owner")]
        [Route("invite/{serviceId}")]
        public ActionResult Invite(InviteMemberBm bm)
        {
            if (!this.ModelState.IsValid)
            {
                return new JsonResult() { Data = new ResultDto("Invalid email format!")  };
            }

            // membership invitation 
            ResultDto result = this.vehicleServiceManager.SendInvitation(bm);

            if (result != null)
            {
                return new JsonResult() { Data = result };
            }

            return new JsonResult() {Data = new ResultDto(null,true) };
        }

        [HttpGet]
        //[Authorize(Roles = "service-member,service-owner")]
        [Route("confirmservice/{serviceId}")]
        //[HandleError(ExceptionType = typeof(ArgumentOutOfRangeException), View = "BadRequestError")] -- does not work with ajax calls
        public ActionResult ConfirmServicedParts(int serviceId)
        {
            if (serviceId < 1)
            {
                return null;
            }

            IEnumerable<RequestCarPartVm> vms = this.vehicleServiceManager.GetUnconfirmedParts(serviceId);

            return this.PartialView(vms);
        }

        [HttpPost]
        //[Authorize(Roles = "service-member,service-owner")]
        [Route("ProcessingServicedParts")]
        public JsonResult ProcessingServicedParts(AnswerBm bm)
        {
            if (bm.Id < 1)
            {
                return new JsonResult() { Data = new ResultDto("Cannot process request!") };
            }

            bool isProcessed = this.vehicleServiceManager.ProcessCarPart(this.GetAppUserId(), bm);

            if (!isProcessed)
            {
                return new JsonResult() { Data = new ResultDto("Cannot process request!") };
            }

            var elementId = "#carpart-request-" + bm.Id;

            return new JsonResult() { Data = new ResultDto(elementId, true) };
        }

        [HttpGet]
        [Route("vote/{id}")]
        [ChildActionOnly]
        public ActionResult VehicleServiceVote(int id)
        {
            return this.PartialView(id);
        }

        [HttpPost]
        [Route("vote")]
        public JsonResult ProcessServiceVote(AnswerBm bm)
        {
            if (bm.Id < 1 || string.IsNullOrWhiteSpace(bm.Message))
            {
                return new JsonResult() { Data = new ResultDto("Request contain invalid data!") };
            }
            
            bool isProcessed = this.vehicleServiceManager.ProcessVote(bm, this.GetAppUserId());

            if (!isProcessed)
            {
                return new JsonResult() { Data = new ResultDto("Cannot process the vote!") };
            }

            int rating = this.vehicleServiceManager.GetRating(bm.Id);

            return new JsonResult() { Data = new ResultDto(rating.ToString(), true) };
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult GetComments(int id)
        {
            IEnumerable<VehicleServiceCommentVm> vms = this.vehicleServiceManager.GetComments(id);

            return this.PartialView(vms);
        }
    }
}