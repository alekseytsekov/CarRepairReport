

namespace CarRepairReport.Areas.Forum.Controllers
{
    using System.Web.Mvc;
    using CarRepairReport.Areas.Forum.Managers;
    using CarRepairReport.Controllers;
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
        [Authorize]
        //[Route("Create")]
        public ActionResult CreatePost()
        {
            var vm = new CreatePostVm();

            vm.Categories = this.forumManager.GetCategories(this.CurrentLanguageCode);

            return this.View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        //[Route("Save")]
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

        [HttpGet]
        [ChildActionOnly]
        [Route("categoriestags")]
        public ActionResult GetCategoryAndTags()
        {
            CategoryTagVm vm = this.forumManager.GetCategoryTagVms(this.CurrentLanguageCode);

            if (vm == null)
            {
                this.Response.StatusCode = 500;
                return this.View("_Custom500InternalServerError");
            }

            vm.LanguageCode = this.CurrentLanguageCode;

            return this.PartialView(vm);
        }

        [HttpGet]
        public ActionResult FilterByCategory(int filter)
        {
            ForumFilterBm bm = new ForumFilterBm();

            bm.Category = this.forumManager.GetCategorySystemNameById(filter);

            if (string.IsNullOrWhiteSpace(bm.Category))
            {
                this.Response.StatusCode = 404;
                return this.View("_Custom404FileNotFound");
            }

            this.forumManager.SetFilter(this.HttpContext.Session, bm);

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult FilterByTag(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                this.Response.StatusCode = 404;
                return this.View("_Custom404FileNotFound");
            }

            ForumFilterBm bm = new ForumFilterBm();

            bm.Tags = filter;

            this.forumManager.SetFilter(this.HttpContext.Session, bm);

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Paging(int page)
        {
            if (page != 1 && page != -1)
            {
                this.Response.StatusCode = 404;
                return this.View("_Custom404FileNotFound");
            }

            this.forumManager.SetPage(page);

            return this.RedirectToAction("Index");
        }
    }
}