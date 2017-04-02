namespace CarRepairReport.Managers
{
    using System.Web;
    using CarRepairReport.Globals;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.Models;
    using CarRepairReport.Services.Interfaces;

    public class LanguageManager : ILanguageManager
    {
        private ILanguageService langService;

        public LanguageManager(ILanguageService langService)
        {
            this.langService = langService;
        }

        public bool SetLangCookie(string lang, string userId, HttpContextBase httpContext)
        {
            if (httpContext.Request.Cookies[Configurations.LangCookieKey] == null)
            {
                var cookie = new HttpCookie(Configurations.LangCookieKey, lang);
                httpContext.Response.Cookies.Add(cookie);
                
            }
            else
            {
                var value = httpContext.Request.Cookies[Configurations.LangCookieKey].Value;
                httpContext.Response.Cookies[Configurations.LangCookieKey].Value = lang;
            }

            bool isSet = false;
            if (!string.IsNullOrWhiteSpace(userId))
            {
                isSet = this.langService.AddUpdateUserLanguage(userId, lang);
            }

            if (!isSet)
            {
                return false;
            }

            return true;
        }

        public Language GetCurrentLang(HttpContextBase httpContext)
        {
            Language lang = null;

            if (httpContext.Request.Cookies[Configurations.LangCookieKey] == null)
            {
                var cookie = new HttpCookie(Configurations.LangCookieKey, Configurations.DefaultLanguageTwoLetterCode);
                httpContext.Response.Cookies.Add(cookie);

                lang = this.langService.GetLanguageByTwoLetterCode(Configurations.DefaultLanguageTwoLetterCode);

                return lang;
            }

            var cookieLangCode = httpContext.Request.Cookies[Configurations.LangCookieKey].Value;

            lang = this.langService.GetLanguageByTwoLetterCode(cookieLangCode);

            return lang;
        }
    }
}