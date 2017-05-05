namespace CarRepairReport.Extensions.HtmlHelpers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using CarRepairReport.Globals;
    using CarRepairReport.Models.Dtos;

    public abstract class LanguageViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        private LanguageResource languageResource;
        private ResourceString resourceString;

        public LanguageResource LR
        {
            get
            {
                if (this.languageResource == null)
                {
                    this.languageResource = (key, langCode) =>
                    {
                        var resources =
                            HttpContext.Current.Cache[CRRConfig.LanguageResourcesCacheCollection] as
                                IEnumerable<LanguageDto>;
                        
                        var value = resources.FirstOrDefault(x => x.Key.ToLower() == key.ToLower() && x.LanguageCode.ToLower() == langCode?.ToLower());

                        if (value == null)
                        {
                            return new ResourceString(key);
                        }

                        return new ResourceString(value.Value);
                    };
                }
                
                return this.languageResource;
            }
        }

        public override void InitHelpers()
        {
            base.InitHelpers();
            //this.LR = new LanguageResource("", "");
        }
    }

    public abstract class LanguageViewPage : System.Web.Mvc.WebViewPage
    {
        private LanguageResource languageResource;
        private ResourceString resourceString;

        public LanguageResource LR
        {
            get
            {
                if (this.languageResource == null)
                {
                    this.languageResource = (key, langCode) =>
                    {
                        var resources =
                            HttpContext.Current.Cache[CRRConfig.LanguageResourcesCacheCollection] as
                                IEnumerable<LanguageDto>;

                        var value = resources.FirstOrDefault(x => x.Key == key.ToLower() && x.LanguageCode == langCode.ToLower());

                        if (value == null)
                        {
                            return new ResourceString(key);
                        }

                        return new ResourceString(value.Value);
                    };
                }

                return this.languageResource;
            }
        }

        public override void InitHelpers()
        {
            base.InitHelpers();
            //this.LR = new LanguageResource("", "");
        }
    }

}