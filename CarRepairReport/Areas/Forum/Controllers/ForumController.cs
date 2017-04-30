using System.Web.Mvc;

namespace CarRepairReport.Areas.Forum.Controllers
{
    [RouteArea("Forum")]
    public class ForumController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}