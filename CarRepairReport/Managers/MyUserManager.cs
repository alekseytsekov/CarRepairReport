namespace CarRepairReport.Managers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using AutoMapper;
    using CarRepairReport.Extensions;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.BindingModels;
    using CarRepairReport.Models.Models;
    using CarRepairReport.Models.ViewModels;
    using CarRepairReport.Services.Interfaces;

    public class MyUserManager : IMyUserManager
    {
        private ILanguageManager langManager;
        private IUserService userService;
        private IAddressService addressService;

        public MyUserManager(ILanguageManager langManager, IUserService userService, IAddressService addressService)
        {
            this.langManager = langManager;
            this.userService = userService;
            this.addressService = addressService;
        }
        public Task CreateMyUserAsync(ApplicationUser appUser, HttpContextBase httpContext)
        {
            return Task.Run(() =>
            {
                var lang = this.langManager.GetCurrentLang(httpContext);

                if (lang == null)
                {
                    lang = new Language()
                    {
                        CreatedOn = DateTime.UtcNow,
                        Name = "English",
                        TwoLetterCode = "en"
                    };
                }

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

        public UserProfileVm GetUserProfileByAppUserId(string userId)
        {
            var user = this.userService.GetUserById(userId);

            if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName))
            {
                return null;
            }

            var vm = Mapper.Map<User, UserProfileVm>(user);

            var address = user.Addresses.FirstOrDefault();

            if (address == null)
            {
                return vm;
            }

            vm.City = address.City.Name.ToCapital();
            vm.Country = address.City.Country.Name.ToCapital();
            vm.Neighborhood = address.Neighborhood.ToCapital();
            vm.StreetName = address.StreetName.ToCapital();

            return vm;
        }

        public bool AddUserDetails(UserProfileBm bm, string userId)
        {
            var user = this.userService.GetUserById(userId);

            user.FirstName = bm.FirstName;
            user.LastName = bm.LastName;
            user.Birthday = bm.Birthday;

            var address = this.addressService.GenerateAddressToUser(bm.Country, bm.City, bm.Neighborhood, bm.StreetName, userId);

            if (address == null)
            {
                //error page
            }

            bool isUpdated = this.userService.Update(user);

            return isUpdated;
        }
    }
}