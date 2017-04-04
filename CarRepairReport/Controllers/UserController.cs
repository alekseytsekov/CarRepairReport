namespace CarRepairReport.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.BindingModels;
    using CarRepairReport.Models.ViewModels;
    using Microsoft.AspNet.Identity;

    [Authorize]
    public class UserController : Controller
    {
        private IMyUserManager userManager;
        private ILanguageManager langManager;

        public UserController(IMyUserManager userManager, ILanguageManager langManager)
        {
            this.userManager = userManager;
            this.langManager = langManager;
        }
        
        // GET: User
        [HttpGet]
        [Route("Profile/Info")]
        public ActionResult Profile()
        {
            var appUserId = this.User.Identity.GetUserId();

            UserProfileVm vm = this.userManager.GetUserProfileByAppUserId(appUserId);

            if (vm == null)
            {
                return this.RedirectToAction("AddProfile");
            }

            return this.View(vm);
        }

        public ActionResult AddProfile()
        {
            var vm = this.TempData["UserProfileVm"] as UserProfileVm;

            if (vm == null)
            {
                vm = new UserProfileVm();
            }
            this.TempData["UserProfileVm"] = null;

            return this.View(vm);
        }

        [HttpPost]
        public ActionResult AddProfile(UserProfileBm bm)
        {
            if (!this.ModelState.IsValid)
            {
                var vm = new UserProfileVm();

                foreach (var kvp in this.ModelState)
                {
                    if (kvp.Value.Errors.Count > 0)
                    {
                        var errorMsg =
                            this.langManager.GetLanguageValueByKey(kvp.Value.Errors.FirstOrDefault().ErrorMessage,
                                this.langManager.GetCurrentLang(this.HttpContext).TwoLetterCode);
                        vm.Errors.Add(kvp.Key, errorMsg);
                    }
                }

                this.TempData["UserProfileVm"] = vm;

                return this.RedirectToAction("AddProfile");
            }

            var isSuccess = this.userManager.AddUserDetails(bm, this.User.Identity.GetUserId());

            if (!isSuccess)
            {
                //error page
            }

            return this.RedirectToAction("Profile");
        }
    }
}