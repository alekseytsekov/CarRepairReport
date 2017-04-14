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
    using CarRepairReport.Models.Models.AddressModels;
    using CarRepairReport.Models.Models.LanguageModels;
    using CarRepairReport.Models.Models.UserModels;
    using CarRepairReport.Models.ViewModels;
    using CarRepairReport.Models.ViewModels.CarVms;
    using CarRepairReport.Services.Interfaces;

    public class MyUserManager : IMyUserManager
    {
        private ILanguageManager langManager;
        private IUserService userService;
        private IAddressService addressService;
        private ICarManager carManager;

        public MyUserManager(ILanguageManager langManager, IUserService userService, IAddressService addressService, ICarManager carManager)
        {
            this.langManager = langManager;
            this.userService = userService;
            this.addressService = addressService;
            this.carManager = carManager;
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
                    //Birthday = new DateTime(1901, 1, 1)
                };

                this.userService.Add(myUser);
            });
        }

        public UserProfileVm GetUserProfileByAppUserId(string appUserId)
        {
            var user = this.userService.GetUserByAppId(appUserId);
            
            if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName))
            {
                return null;
            }

            var vm = Mapper.Map<User, UserProfileVm>(user);

            var address = user.Addresses.FirstOrDefault(x=> x.IsPrimary && x.Users.Any(u => u.ApplicationUserId == appUserId));

            //var users = this.userService.GetAllUsers();
            //var addresses = this.addressService.GetAllAddresses();

            if (address == null)
            {
                return vm;
            }

            vm.CityName = address.City.Name.ToCapital();
            vm.CountryName = address.City.Country.Name.ToCapital();

            foreach (var car in user.Cars.Where(x=> !x.IsDeleted))
            {
                var vmCar = this.carManager.MapToSimpleVm(car);

                vm.Cars.Add(vmCar);
            }

            return vm;
        }

        public bool AddUserDetails(UserProfileBm bm, string appUserId)
        {
            var isUserExists = this.userService.IsUserExists(appUserId);

            if (!isUserExists)
            {
                return false;
            }

            var isPrimary = true;

            var address = this.addressService.GenerateAddressToUser(bm.CountryName, bm.CityName, bm.Neighborhood, bm.StreetName, appUserId, isPrimary);

            if (address == null)
            {
                return false;
            }

            bool isUpdated = this.userService.UpdatePersonalInfo(bm.FirstName, bm.LastName, appUserId);

            return isUpdated;
        }
        
        public bool EditUserPersonalDetails(EditUserBm bm, string appUserId)
        {
            var model = Mapper.Map<EditUserBm, UserProfileBm>(bm);

            var result = this.AddUserDetails(model, appUserId);

            return result;
        }

        public EditUserVm GetEditModelByAppId(string appUserId)
        {
            User user = this.userService.GetUserByAppId(appUserId);

            if (user == null)
            {
                var model = new EditUserVm();
                model.Errors.Add("error", null);
                return model;
            }

            var address = user.Addresses.FirstOrDefault(x => x.IsPrimary);

            var cityName = string.Empty;
            var countryName = string.Empty;

            if (address != null)
            {
                cityName = address.City.Name;
                countryName = address.City.Country.Name;
            }

            var vm = new EditUserVm()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                CityName = cityName,
                CountryName = countryName
            };

            return vm;
        }
        
    }
}