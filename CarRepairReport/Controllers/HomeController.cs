namespace CarRepairReport.Controllers
{
    using System.Web.Mvc;
    using CarRepairReport.Data;
    using CarRepairReport.Models.AppModels;

    public class HomeController : Controller
    {
        private ICarRepairReportData crrd;

        public HomeController(ICarRepairReportData crrd)
        {
            this.crrd = crrd;
        }

        public ActionResult Index()
        {
            //var user = this.crrd.Users.FirstOrDefault();

            //user.UserName = "Al The Great";

            //var lang = new Language()
            //{
            //    Name = "Bulgarian",
            //    TwoLetterCode = "BG"
            //};

            //this.crrd.Languages.Add(lang);
            //this.crrd.Commit();

            return this.View();
        }

        public ActionResult About()
        {
            this.ViewBag.Message = "Your application description page.";
            
            return this.View();
        }

        public ActionResult Contact()
        {
            this.ViewBag.Message = "Your contact page.";

            return this.View();
        }
    }
}