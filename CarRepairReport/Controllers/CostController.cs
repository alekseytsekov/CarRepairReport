namespace CarRepairReport.Controllers
{
    using System.Web.Mvc;

    [Authorize]
    [RoutePrefix("Cost")]
    public class CostController : Controller
    {
        [Route]
        [HttpPost]
        public ActionResult Cost()
        {
            return null;
        }
    }
}