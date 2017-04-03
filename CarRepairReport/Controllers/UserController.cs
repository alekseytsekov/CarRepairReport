namespace CarRepairReport.Controllers
{
    using System.Web.Mvc;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.ViewModels;
    using Microsoft.AspNet.Identity;

    [Authorize]
    public class UserController : Controller
    {
        private IMyUserManager userManager;

        public UserController(IMyUserManager userManager)
        {
            this.userManager = userManager;
        }

        

        // GET: User
        [HttpGet]
        public ActionResult Profile()
        {
            var userId = this.User.Identity.GetUserId();

            UserProfileVm vm = this.userManager.GetUserProfileById(userId);

            if (true)
            {
                return this.RedirectToAction("AddProfile");
            }

            return View();
        }

        public ActionResult AddProfile()
        {
            return null;
        }
    }
}