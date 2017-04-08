namespace CarRepairReport.Controllers
{
    using System.Web.Mvc;
    using CarRepairReport.Models.ViewModels.CarVms;

    [Authorize]
    [RoutePrefix("Cars")]
    public class CarController : Controller
    {
        [HttpGet]
        [Route("Add")]
        public ActionResult Add()
        {
            var vm = new CreateCarVm();

            return View(vm);
        }

        [HttpPost]
        [Route("Add")]
        public ActionResult Add(int a)
        {
            return View();
        }

        [HttpGet]
        [Route("Edit")]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [Route("Edit")]
        public ActionResult Edit(int a)
        {
            return View();
        }

        [HttpGet]
        [Route("Remove")]
        public ActionResult Remove()
        {
            return View();
        }

        [HttpPost]
        [Route("Remove")]
        public ActionResult Remove(int a)
        {
            return View();
        }
    }
}