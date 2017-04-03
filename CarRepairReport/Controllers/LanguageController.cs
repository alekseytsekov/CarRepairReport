namespace CarRepairReport.Controllers
{
    using System.Globalization;
    using System.Web.Mvc;
    using CarRepairReport.Managers.Interfaces;
    using Microsoft.Ajax.Utilities;
    using Microsoft.AspNet.Identity;
    using Newtonsoft.Json;

    public class LanguageController : Controller
    {
        private ILanguageManager langManager;

        public LanguageController(ILanguageManager langManager)
        {
            this.langManager = langManager;
        }

        

        [HttpGet]
        public ActionResult Set(string langValue, string returnUrl)
        {
            var userId = this.User.Identity.GetUserId();

            bool isSuccess = this.langManager.SetLangCookie(langValue, userId, this.HttpContext);

            this.SetCulture(langValue);

            return this.Redirect(returnUrl);
        }

        public void SetCulture(string lang)
        {
            var langValue = string.Empty;

            switch (lang)
            {
                case "en":
                    langValue = "en-EN";
                    break;
                case "bg":
                    langValue = "bg-BG";
                    break;
                case "ru":
                    langValue = "ru-RU";
                    break;
                default:return;
            }

            CultureInfo ci = new CultureInfo(langValue);
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
        }
    }
}