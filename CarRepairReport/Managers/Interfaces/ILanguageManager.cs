using System.Web;

namespace CarRepairReport.Managers.Interfaces
{
    using CarRepairReport.Models.Models;
    using CarRepairReport.Models.Models.LanguageModels;

    public interface ILanguageManager
    {
        bool SetLangCookie(string lang, string userId, HttpContextBase httpContext);
        Language GetCurrentLang(HttpContextBase httpContext);
        string GetLanguageValueByKey(string langKey, string langCode);
    }
}
