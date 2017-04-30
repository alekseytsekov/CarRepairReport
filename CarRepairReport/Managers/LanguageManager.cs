namespace CarRepairReport.Managers
{
    using System.Web;
    using CarRepairReport.Globals;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.Models;
    using CarRepairReport.Models.Models.LanguageModels;
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
            if (httpContext.Request.Cookies[CRRConfig.LangCookieKey] == null)
            {
                var cookie = new HttpCookie(CRRConfig.LangCookieKey, lang);
                httpContext.Response.Cookies.Add(cookie);
                
            }
            else
            {
                var value = httpContext.Request.Cookies[CRRConfig.LangCookieKey].Value;
                httpContext.Response.Cookies[CRRConfig.LangCookieKey].Value = lang;
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

            if (httpContext.Request.Cookies[CRRConfig.LangCookieKey] == null)
            {
                var cookie = new HttpCookie(CRRConfig.LangCookieKey, CRRConfig.DefaultLanguageTwoLetterCode);
                httpContext.Response.Cookies.Add(cookie);

                lang = this.langService.GetLanguageByTwoLetterCode(CRRConfig.DefaultLanguageTwoLetterCode);

                return lang;
            }

            var cookieLangCode = httpContext.Request.Cookies[CRRConfig.LangCookieKey].Value;

            lang = this.langService.GetLanguageByTwoLetterCode(cookieLangCode);

            return lang;
        }

        public string GetLanguageValueByKey(string langKey, string langCode)
        {
            string langValue = this.langService.GetLanguageValueByKey(langKey, langCode);

            return langValue;
        }
    }
}