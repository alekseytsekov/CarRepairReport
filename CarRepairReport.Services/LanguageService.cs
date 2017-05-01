namespace CarRepairReport.Services
{
    using System;
    using System.Collections.Generic;
    using CarRepairReport.Data;
    using CarRepairReport.Models.Models;
    using CarRepairReport.Models.Models.LanguageModels;
    using CarRepairReport.Services.Interfaces;

    public class LanguageService : Service, ILanguageService
    {
        public LanguageService(ICarRepairReportData context) : base(context)
        {
        }

        public bool AddUpdateUserLanguage(string userId, string twoLetterCode)
        {
            twoLetterCode = twoLetterCode.ToLower();

            var user = this.context.MyUsers.FirstOrDefault(x => x.ApplicationUserId == userId);

            if (user == null)
            {
                return false;
            }

            var sysLang = this.GetLanguageByTwoLetterCode(twoLetterCode);

            var userLang = user.UserSetting.Language;

            userLang.Name = sysLang.Name;
            userLang.TwoLetterCode = sysLang.TwoLetterCode;

            //switch (lang)
            //{
            //    case "en":
            //        userLang.Name = "English";
            //        userLang.TwoLetterCode = "en";
            //        break;
            //    case "bg":
            //        userLang.Name = "Bulgarian";
            //        userLang.TwoLetterCode = "en";
            //        break;
            //    case "ru":
            //        userLang.Name = "Russian";
            //        userLang.TwoLetterCode = "en";
            //        break;
            //    default:
            //        return false;
            //}

            try
            {
                this.context.Commit();
            }
            catch (Exception ex)
            {
                return this.LogError(ex);
            }
            
            return true;
        }

        public Language GetLanguageByTwoLetterCode(string twoLetterCode)
        {
            var lang = this.context.Languages.FirstOrDefault(x => x.IsSystemLanguage && x.TwoLetterCode == twoLetterCode);
            
            return lang;
        }

        public string GetLanguageValueByKey(string langKey, string langCode)
        {
            var langValue = context.LanguageValues.FirstOrDefault(x => x.LangTwoLetterCode == langCode && x.Key == langKey);
            var value = string.Empty;

            if (langValue == null)
            {
                value = langKey;
            }
            else
            {
                value = langValue.Value;
            }

            return value;
        }

        public IEnumerable<LanguageValue> GetAllResources()
        {
            return this.context.LanguageValues.All();
        }
    }
}
