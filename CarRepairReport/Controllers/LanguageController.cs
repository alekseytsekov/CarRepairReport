namespace CarRepairReport.Controllers
{
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

        

        [HttpPost]
        public JsonResult Set(string langValue)
        {
            bool isSuccess = false;
            var result = "{result : " + isSuccess + "}";

            if (!this.Request.IsAjaxRequest())
            {
                return Json(result);
            }

            var userId = this.User.Identity.GetUserId();
            
            isSuccess = this.langManager.SetLangCookie(langValue, userId, this.HttpContext);

            result = "{result : " + isSuccess + "}";

            return this.Json(result);
        }

        public JsonResult Set( )
        {
            return Json("");
        }
    }
}