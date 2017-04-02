namespace CarRepairReport.Managers
{
    using System;
    using System.Threading.Tasks;
    using System.Web;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.Models;
    using CarRepairReport.Services.Interfaces;

    public class MyUserManager : IMyUserManager
    {
        private ILanguageManager langManager;
        private IUserService userService;

        public MyUserManager(ILanguageManager langManager, IUserService userService)
        {
            this.langManager = langManager;
            this.userService = userService;
        }
        public Task CreateMyUserAsync(ApplicationUser appUser, HttpContextBase httpContext)
        {
            return Task.Run(() =>
            {
                var lang = this.langManager.GetCurrentLang(httpContext);

                var language = new Language()
                {
                    Name = lang.Name,
                    TwoLetterCode = lang.TwoLetterCode
                };

                var userSetting = new UserSetting()
                {
                    Language = language,
                    LanguageId = language.Id
                };

                var myUser = new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserSetting = userSetting,
                    //ApplicationUser = appUser,
                    ApplicationUserId = appUser.Id,
                    Birthday = new DateTime(1901, 1, 1)
                };

                this.userService.Add(myUser);
            });
        }
    }
}