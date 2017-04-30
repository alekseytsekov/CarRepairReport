namespace CarRepairReport.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using AutoMapper;
    using CarRepairReport.Extensions;
    using CarRepairReport.Globals;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models;
    using CarRepairReport.Models.BindingModels;
    using CarRepairReport.Models.BindingModels.CommonBms;
    using CarRepairReport.Models.Dtos;
    using CarRepairReport.Models.Models.CommonModels;
    using CarRepairReport.Models.Models.LanguageModels;
    using CarRepairReport.Models.Models.UserModels;
    using CarRepairReport.Models.ViewModels.Commons;
    using CarRepairReport.Models.ViewModels.UserVms;
    using CarRepairReport.Services.Interfaces;
    using CloudStorageApi;

    public class MyUserManager : IMyUserManager
    {
        private ILanguageManager langManager;
        private IUserService userService;
        private IAddressService addressService;
        private ICarManager carManager;
        private IVehicleServiceService vehicleService;
        private ICommonService commonService;
        private ICloudStorage cloudStorage;

        public MyUserManager(ILanguageManager langManager, 
                             IUserService userService, 
                             IAddressService addressService, 
                             ICarManager carManager,
                             IVehicleServiceService vehicleService,
                             ICommonService commonService,
                             ICloudStorage cloudStorage)
        {
            this.langManager = langManager;
            this.userService = userService;
            this.addressService = addressService;
            this.carManager = carManager;
            this.vehicleService = vehicleService;
            this.commonService = commonService;
            this.cloudStorage = cloudStorage;
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

            var address = this.addressService.GenerateAddress(bm.CountryName, bm.CityName, bm.Neighborhood, bm.StreetName, appUserId, isPrimary, AddressType.Home);

            if (address == null)
            {
                return false;
            }

            bool isUpdated = this.userService.UpdatePersonalInfo(bm.FirstName, bm.LastName, bm.ImageUrl, appUserId);

            return isUpdated;
        }
        
        public bool EditUserPersonalDetails(EditUserBm bm, string appUserId)
        {
            var model = Mapper.Map<EditUserBm, UserProfileBm>(bm);

            //var oldImgUrl = this.userService.GetUserImgUrl(appUserId);

            if (this.CanUploadImage(bm.Image))
            {
                var newImgUrl = this.GetDownloadbleLink(bm.Image, bm.ServerPath);

                model.ImageUrl = newImgUrl;
            }
            
            var result = this.AddUserDetails(model, appUserId);

            return result;
        }

        public ResultDto RegisterVehicleService(CreateCarServiceBm bm, string appUserId)
        {
            bool isUniqueName = this.vehicleService.IsServiceNameUnique(bm.Name);

            if (!isUniqueName)
            {
                return new ResultDto("Already exists service with that name!");
            }

            var address = this.addressService.GenerateAddress(bm.Country, bm.City, string.Empty, bm.StreetName, appUserId, true, AddressType.Work);

            var workingDays = string.Empty;
            var nonWorkingDays = string.Empty;

            foreach (var wDay in bm.WorkingDays)
            {
                if (wDay.IsChecked)
                {
                    workingDays += wDay.StringValue + ", ";
                }
                else
                {
                    nonWorkingDays += wDay.StringValue + ", ";
                }
            }

            workingDays = workingDays.Trim(' ').Trim(',');
            nonWorkingDays = nonWorkingDays.Trim(' ').Trim(',');

            var vehicleService = new VehicleService()
            {
                Name = bm.Name.ToLower(),
                Address = address,
                AddressId = address.Id,
                Description = bm.Description,
                //LogoUrl = bm.logo
                WorkingTime = "{0}: " + bm.StartWorkingTime + " {1}: " + bm.EndWorkingTime,
                WorkingDays = workingDays,
                NonWorkingDays = nonWorkingDays
            };

            var user = this.userService.GetUserByAppId(appUserId);

            user.IsVehicleServiceOwner = true;
            user.VehicleService = vehicleService;
            vehicleService.ServiceMembers.Add(user);

            var isAdded = this.vehicleService.AddVehicleService(vehicleService);

            if (!isAdded)
            {
                return new ResultDto("Something goes wrong!") ;
            }

            return null;
        }

        public ICollection<MembershipInvitationVm> GetInvitations(string email)
        {
            var invitations = this.commonService.GetInvitationsByEmail(email);

            var vms = Mapper.Map<IEnumerable<MembershipInvitation>, IEnumerable<MembershipInvitationVm>>(invitations);

            return vms.ToList();
        }

        public bool ProcessMembershipInvitation(AnswerBm bm, string appUserId)
        {
            var membershipInvitation = this.commonService.GetMembershipInvitationById(bm.Id);

            if (membershipInvitation == null)
            {
                return false;
            }

            var user =
                this.userService.GetAllUsers().FirstOrDefault(x => x.ApplicationUser.Email == membershipInvitation.MemberEmail);

            if (user == null)
            {
                return false;
            }

            // check is it same user or same invitation
            if (user.ApplicationUserId != appUserId)
            {
                return false;
            }

            var vehicleServiceEntity = this.vehicleService.GetVehiceService(membershipInvitation.VehicleServiceId);

            if (vehicleServiceEntity == null)
            {
                return false;   
            }

            if (user.VehicleServiceId == vehicleServiceEntity.Id)
            {
                return false;
            }

            if (bm.IsAccepted)
            {
                user.VehicleService = vehicleServiceEntity;
                user.VehicleServiceId = vehicleServiceEntity.Id;
                vehicleServiceEntity.ServiceMembers.Add(user);
                membershipInvitation.IsAccepted = true;
                membershipInvitation.IsDeleted = true;
                
            }
            else
            {
                membershipInvitation.IsAccepted = false;
                membershipInvitation.IsDeleted = true;
            }

            var isUpdated = this.userService.Update();

            if (!isUpdated)
            {
                return false;
            }

            return true;
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

        private string GetDownloadbleLink(HttpPostedFileBase image, string path)
        {
            this.cloudStorage.StartService(path);

            var fileId = this.cloudStorage.UploadFile(image);

            if (string.IsNullOrEmpty(fileId))
            {
                return null;
            }

            return CRRConfig.GoogleDownloadLink + fileId;
        }

        private bool CanUploadImage(HttpPostedFileBase image)
        {
            var isNull = image != null;
            var sizeCheck = image.ContentLength <= CRRConfig.MaxImageSize;
            var fileType = image.ContentType.ToLower() == "image/jpg" || image.ContentType == "image/jpeg";
            
            return isNull && sizeCheck && fileType;
        }
    }
}