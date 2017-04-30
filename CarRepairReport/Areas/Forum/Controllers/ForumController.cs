using System.Web.Mvc;

namespace CarRepairReport.Areas.Forum.Controllers
{
    using CarRepairReport.Controllers;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.BindingModels.ForumBms;
    using CarRepairReport.Models.ViewModels.ForumVm;

    [RouteArea("Forum")]
    public class ForumController : BaseController
    {
        public ForumController(IMyUserManager myUserManager) : base(myUserManager)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            var vm = new ForumVm();
            vm.LanguageCode = "bg";

            return this.View(vm);
        }
        
        [HttpPost]
        public ActionResult FilterPosts([Bind(Prefix = "FilterVm")]ForumFilterBm bm)
        {
            
            return this.PartialView();
        }

        [HttpGet]
        public ActionResult CreatePost()
        {
            return this.View();
        }
    }
}