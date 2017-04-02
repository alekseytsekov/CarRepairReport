namespace CarRepairReport.Services
{
    using CarRepairReport.Data;
    using CarRepairReport.Models.Models;
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

            this.context.Commit();

            return true;
        }

        public Language GetLanguageByTwoLetterCode(string twoLetterCode)
        {
            var lang = this.context.Languages.FirstOrDefault(x => x.IsSystemLanguage && x.TwoLetterCode == twoLetterCode);

            return lang;
        }
    }
}
