namespace CarRepairReport.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using CarRepairReport.Globals;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.BindingModels.CommonBms;
    using CarRepairReport.Models.BindingModels.VehicleServiceBms;
    using CarRepairReport.Models.Dtos;
    using CarRepairReport.Models.ViewModels.CarVms;
    using CarRepairReport.Models.ViewModels.ServiceVms;
    using CarRepairReport.Models.ViewModels.UserVms;


    public class VehicleServiceController : BaseController
    {
        private IVehicleServiceManager vehicleServiceManager;

        public VehicleServiceController(IVehicleServiceManager vehicleServiceManager, IMyUserManager myUserManager, ILanguageManager languageManager) : base(myUserManager, languageManager)
        {
            this.vehicleServiceManager = vehicleServiceManager;
        }
        
        [HttpGet]
        [Route("service/{id}")]
        //[OutputCache(Duration = 60 * 20)]
        public ActionResult VehicleService(int id)
        {
            VehicleServiceVm vm = this.vehicleServiceManager.GetVm(id, this.GetAppUserId);

            if (vm == null)
            {
                // error page not found
            }

            vm.WorkingTime = string.Format(vm.WorkingTime, "From", "To");

            vm.LanguageCode = this.CurrentLanguageCode;

            return this.View(vm);
        }

        [ChildActionOnly]
        public ActionResult TopVehicleServices()
        {
            ICollection<ShortServiceVm> vms = this.vehicleServiceManager
                .GetTopServicesShortInfo(CRRConfig.NumberOfTopVehicleServiceInHomeView);

            return this.PartialView("_TopVehicleServices", vms);
        }

        [HttpGet]
        [Authorize]
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

            vm.LanguageCode = this.CurrentLanguageCode;

            return this.View(vm);
        }

        [HttpPost]
        [Authorize]
        //[Authorize(Roles = "service-member,service-owner")]
        [Route("service/{id}/manage")]
        public ActionResult Manage()
        {
            return this.RedirectToAction("Manage", 4);
        }

        [HttpGet]
        [Authorize]
        //[Authorize(Roles = "service-member,service-owner")]
        [Route("members/{serviceId}")]
        public ActionResult Members(int serviceId)
        {
            var vm = new InviteMemberVm() {Id = serviceId};

            vm.LanguageCode = this.CurrentLanguageCode;

            return this.PartialView(vm);
        }
        
        [HttpPost]
        [Authorize]
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
        [Authorize]
        //[Authorize(Roles = "service-member,service-owner")]
        [Route("confirmservice/{serviceId}")]
        //[HandleError(ExceptionType = typeof(ArgumentOutOfRangeException), View = "BadRequestError")] -- does not work with ajax calls
        public ActionResult ConfirmServicedParts(int serviceId)
        {
            if (serviceId < 1)
            {
                this.Response.StatusCode = 400;
                return this.View("_Custom400BadRequestError");
            }

            IEnumerable<RequestCarPartVm> vms = this.vehicleServiceManager.GetUnconfirmedParts(serviceId);

            return this.PartialView(vms);
        }

        [HttpPost]
        [Authorize]
        //[Authorize(Roles = "service-member,service-owner")]
        [Route("ProcessingServicedParts")]
        public JsonResult ProcessingServicedParts(AnswerBm bm)
        {
            if (bm.Id < 1)
            {
                return new JsonResult() { Data = new ResultDto("Cannot process request!") };
            }

            bool isProcessed = this.vehicleServiceManager.ProcessCarPart(this.GetAppUserId, bm);

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
            if (id < 1)
            {
                this.Response.StatusCode = 400;
                return this.View("_Custom400BadRequestError");
            }

            return this.PartialView(id);
        }

        [HttpPost]
        [Route("vote")]
        [Authorize]
        public JsonResult ProcessServiceVote(AnswerBm bm)
        {
            if (bm.Id < 1 || string.IsNullOrWhiteSpace(bm.Message))
            {
                return new JsonResult() { Data = new ResultDto("Request contain invalid data!") };
            }
            
            bool isProcessed = this.vehicleServiceManager.ProcessVote(bm, this.GetAppUserId);

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
            if (id < 1)
            {
                this.Response.StatusCode = 400;
                return this.View("_Custom400BadRequestError");
            }

            IEnumerable<VehicleServiceCommentVm> vms = this.vehicleServiceManager.GetComments(id);

            return this.PartialView(vms);
        }

        [HttpGet]
        [Route("services")]
        public ActionResult GetAllVehicleServiceShortInfo()
        {
            var vms = this.vehicleServiceManager.GetTopServicesShortInfo(int.MaxValue).OrderBy(x => x.Name);

            return this.View(vms);
        }

        [HttpGet, Route("GetMembers"), ChildActionOnly, Authorize]
        public ActionResult GetMembers(int serviceId)
        {
            if (serviceId < 0)
            {
                this.Response.StatusCode = 400;
                return this.View("_Custom400BadRequestError");
            }

            MembersWrapperVm vm = this.vehicleServiceManager.GetMembers(serviceId, this.GetAppUserId);

            if (vm == null)
            {
                this.Response.StatusCode = 400;
                return this.View("_Custom400BadRequestError");
            }

            vm.LanguageCode = this.CurrentLanguageCode;

            return this.PartialView(vm);
        }
    }
}