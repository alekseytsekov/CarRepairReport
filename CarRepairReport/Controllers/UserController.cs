namespace CarRepairReport.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.BindingModels;
    using CarRepairReport.Models.Dtos;
    using CarRepairReport.Models.ViewModels;
    using CarRepairReport.Models.ViewModels.UserVms;
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
        public ActionResult UserProfile()
        {
            var appUserId = this.User.Identity.GetUserId();

            UserProfileVm vm = this.userManager.GetUserProfileByAppUserId(appUserId);
            
            if (vm == null)
            {
                return this.RedirectToAction("AddProfile");
            }

            return this.View(vm);
        }

        [HttpGet]
        public ActionResult AddProfile()
        {
            var vm = this.TempData["errorModel"] as UserProfileVm;

            if (vm == null)
            {
                vm = new UserProfileVm();
            }
            this.TempData["errorModel"] = null;

            return this.View(vm);
        }

        [HttpPost]
        public ActionResult AddProfile(UserProfileBm bm)
        {
            if (!this.ModelState.IsValid)
            {
                var vm = new UserProfileVm();
                vm.FirstName = bm.FirstName;
                vm.LastName = bm.LastName;
                vm.CityName = bm.CityName;
                vm.CountryName = bm.CountryName;

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

                this.TempData["errorModel"] = vm;

                return this.RedirectToAction("AddProfile");
            }

            var isSuccess = this.userManager.AddUserDetails(bm, this.User.Identity.GetUserId());

            if (!isSuccess)
            {
                //error page
            }

            return this.RedirectToAction("UserProfile");
        }

        [HttpGet]
        [Route("Profile/Edit")]
        public ActionResult Edit()
        {
            var errorModel = this.TempData["errorModel"] as EditUserVm;

            if (errorModel != null)
            {
                this.TempData["errorModel"] = null;
                return this.View(errorModel);
            }
            
            var userId = this.User.Identity.GetUserId();

            EditUserVm vm = this.userManager.GetEditModelByAppId(userId);

            if (vm.Errors.Any())
            {
                if (vm.Errors.ContainsKey("error"))
                {
                    //error
                    return this.View();
                }
            }

            return this.View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Profile/Edit")]
        public ActionResult Edit(EditUserBm bm)
        {
            if (!this.ModelState.IsValid)
            {
                //error page

                //var errorModel = new EditUserVm();

                //foreach (var kvp in this.ModelState)
                //{
                //    if (kvp.Value.Errors.Count > 0)
                //    {
                //        var errorMsg =
                //            this.langManager.GetLanguageValueByKey(kvp.Value.Errors.FirstOrDefault().ErrorMessage,
                //                this.langManager.GetCurrentLang(this.HttpContext).TwoLetterCode);
                //        errorModel.Errors.Add(kvp.Key, errorMsg);
                //    }
                //}

                //this.TempData["errorModel"] = errorModel;

                //return this.RedirectToAction("Edit");
            }

            var result = this.userManager.EditUserPersonalDetails(bm, this.User.Identity.GetUserId());

            if (!result)
            {
                //error
            }

            return this.RedirectToAction("UserProfile");
        }

        [HttpGet]
        [Route("RegisterCarService")]
        public ActionResult RegisterCarService()
        {
            if (false)
            {
                // already is owner .. return error page
            }
            var vm = new CreateCarServiceVm();

            var wd = new []
            {
                new CheckBoxDto() { IntValue = 0, StringValue = "Monday"},
                new CheckBoxDto() { IntValue = 1, StringValue = "Tuesday"},
                new CheckBoxDto() { IntValue = 2, StringValue = "Wednesday"},
                new CheckBoxDto() { IntValue = 3, StringValue = "Thursday"},
                new CheckBoxDto() { IntValue = 4, StringValue = "Friday"},
                new CheckBoxDto() { IntValue = 5, StringValue = "Saturday"},
                new CheckBoxDto() { IntValue = 6, StringValue = "Sunday"},
            };

            var hd = new[]
            {
                new CheckBoxDto() { IntValue = 0, StringValue = "Monday"},
                new CheckBoxDto() { IntValue = 1, StringValue = "Tuesday"},
                new CheckBoxDto() { IntValue = 2, StringValue = "Wednesday"},
                new CheckBoxDto() { IntValue = 3, StringValue = "Thursday"},
                new CheckBoxDto() { IntValue = 4, StringValue = "Friday"},
                new CheckBoxDto() { IntValue = 5, StringValue = "Saturday"},
                new CheckBoxDto() { IntValue = 6, StringValue = "Sunday"},
            };

            vm.WorkingDays = wd;
            vm.NonWorkingDays = hd;

            return this.View(vm);
        }
    }
}