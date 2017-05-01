using System.Web.Mvc;

namespace CarRepairReport.Areas.Forum.Controllers
{
    using System.Web.Http;
    using CarRepairReport.Areas.Forum.Managers;
    using CarRepairReport.Controllers;
    using CarRepairReport.Globals;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.BindingModels.ForumBms;
    using CarRepairReport.Models.ViewModels.ForumVm;

    [RouteArea("Forum")]
    public class ForumController : BaseController
    {
        private IForumManager forumManager;
        public ForumController(IForumManager forumManager, IMyUserManager myUserManager, ILanguageManager languageManager) : base(myUserManager,languageManager)
        {
            this.forumManager = forumManager;
        }

        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            var vm = new ForumVm();
            vm.LanguageCode = "bg";

            return this.View(vm);
        }
        
        [HttpPost]
        public ActionResult FilterPosts([Bind(Prefix = "FilterVm")]ForumFilterBm bm)
        {
            this.forumManager.SetFilter(this.HttpContext.Session, bm);
            
            return this.RedirectToAction("Posts");
        }

        [HttpGet]
        [Route("posts")]
        //[ChildActionOnly]
        
        public ActionResult Posts()
        {
            PostWrapperVm vm = this.forumManager.GetPosts(this.HttpContext.Session);
            
            return this.PartialView(vm);
        }

        [HttpGet]
        [Route("post/{title}")]
        public ActionResult Post(string title)
        {
            ViewPostVm vm = this.forumManager.GetPost(title);

            if (vm == null)
            {
                this.Response.StatusCode = 400;
                return this.View("_Custom400BadRequestError");
            }

            return this.View(vm);
        }

        [HttpGet]
        //[Authorize]
        public ActionResult CreatePost()
        {
            var vm = new CreatePostVm();

            vm.Categories = this.forumManager.GetCategories(this.CurrentLanguageCode);

            return this.View(vm);
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public ActionResult CreatePost(CreatePostBm bm)
        {
            if (!this.ModelState.IsValid)
            {
                this.Response.StatusCode = 400;
                return this.View("_Custom400BadRequestError");
            }
            
            bool isAdded = this.forumManager.CreatePost(bm, this.GetAppUserId, this.CurrentLanguageCode);

            if (!isAdded)
            {
                this.Response.StatusCode = 500;
                return this.View("_Custom500InternalServerError");
            }

            //return this.RedirectToRoute("/forum");
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetCategoryAndTags()
        {
            CategoryTagVm vm = new CategoryTagVm();

            return this.PartialView(vm);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult PostChildren(ViewPostVm bm)
        {
            return this.PartialView(bm);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult PostChildrenId(int id)
        {
            ViewPostVm vm = this.forumManager.GetPostById(id);

            if (vm == null)
            {
                this.Response.StatusCode = 400;
                return this.View("_Custom400BadRequestError");
            }

            return this.PartialView("PostChildren", vm);
        }

        [HttpPost]
        [Route("setanswer")]
        [Authorize]
        public ActionResult SetAnswer(PostAnswerBm bm)
        {
            if (string.IsNullOrWhiteSpace(bm.Content) || bm.Id < 1)
            {
                this.Response.StatusCode = 400;
                return this.View("_Custom400BadRequestError");
            }

            string webTitleLink = this.forumManager.CreateAnswer(bm, this.GetAppUserId);

            if (string.IsNullOrWhiteSpace(webTitleLink))
            {
                this.Response.StatusCode = 400;
                return this.View("_Custom400BadRequestError");
            }

            return this.RedirectToAction("Post", new {title = webTitleLink });
        }
    }
}