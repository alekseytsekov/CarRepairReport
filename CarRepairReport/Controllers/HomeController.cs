namespace CarRepairReport.Controllers
{
    using System.Web.Mvc;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.BindingModels;
    using CarRepairReport.Models.ViewModels.Commons;
    using Microsoft.AspNet.Identity;

    public class HomeController : BaseController
    {
        private ICarManager carManager;
        //private IVehicleServiceManager vehicleServiceManager;

        public HomeController(ICarManager carManager, IMyUserManager myUserManager, ILanguageManager languageManager) : base(myUserManager, languageManager)
        {
            this.carManager = carManager;
            //this.vehicleServiceManager = vehicleServiceManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            var vm = new HomeVm();

            vm.InvestPart = this.PrepareInvestPartModel();
            //vm.Invitations = this.GetInvitations();
            //vm.ShortServices = this.TakeVehicleServices();
            
            return this.View(vm);
        }
        
        private InvestPartBm PrepareInvestPartModel()
        {
            var investPart = new InvestPartBm();

            investPart.VehicleServices.Add("By Me");

            var serviceNames = this.carManager.GetVehicleServiceNames();

            foreach (var serviceName in serviceNames)
            {
                investPart.VehicleServices.Add(serviceName);
            }

            var userId = this.User.Identity.GetUserId();
            investPart.CarNames = this.carManager.GetCarNames(userId);
            
            return investPart;
        }

        //public ActionResult About()
        //{
        //    this.ViewBag.Message = "Your application description page.";
            
        //    return this.View();
        //}

        //public ActionResult Contact()
        //{
        //    this.ViewBag.Message = "Your contact page.";

        //    return this.View();
        //}
    }
}