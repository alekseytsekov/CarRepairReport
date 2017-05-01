

namespace CarRepairReport.Controllers
{
    using System;
    using System.Web.Mvc;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.BindingModels.GarageBms;
    using CarRepairReport.Models.ViewModels.GarageVms;

    [Authorize]
    public class GarageController : BaseController
    {
        private ICarManager carManager;

        public GarageController(IMyUserManager myUserManager, ICarManager carManager, ILanguageManager languageManager) : base(myUserManager, languageManager)
        {
            this.carManager = carManager;
        }

        [HttpGet]
        [Route("cars")]
        [HandleError(ExceptionType = typeof(ArgumentException), View = "BadRequestError")]
        public ActionResult Cars(GarageBm bm)
        {
            GarageVm vm = new GarageVm();

            vm.AvailableCars = this.carManager.GetCarNames(this.GetAppUserId);

            if (bm.SelectedCar > 0)
            {
                vm.Car = this.carManager.GetFullCarInfo(bm.SelectedCar, this.GetAppUserId);

                if (vm.Car == null)
                {
                    throw new ArgumentException();
                }
            }
            
            return this.View(vm);
        }
    }
}