namespace CarRepairReport.HtmlHelpers
{
    using System.Web.Mvc;
    using CarRepairReport.Data;

    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString LangValue(this HtmlHelper helper, string languageKey, string langCode)
        {
            var context = DependencyResolver.Current.GetService<CarRepairReportData>();
            
            var langValue = context.LanguageValues.FirstOrDefault(x => x.LangTwoLetterCode == langCode && x.Key == languageKey);
            var value = string.Empty;

            if (langValue == null)
            {
                value = languageKey;
            }
            else
            {
                value = langValue.Value;
            }

            return new MvcHtmlString(value);
        }
    }
}