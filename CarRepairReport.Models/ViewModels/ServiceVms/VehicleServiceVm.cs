namespace CarRepairReport.Models.ViewModels.ServiceVms
{
    using System.Collections.Generic;
    using CarRepairReport.Models.ViewModels.CarVms;

    public class VehicleServiceVm : ViewBindingModel
    {
        public VehicleServiceVm()
        {
            this.CarParts = new HashSet<BasicCarPartVm>();
        }

        public int Id { get; set; }

        public bool HasUserManagementRights { get; set; }

        public string Name { get; set; }

        public string Description { get; set; } 

        public string StreetName { get; set; }

        public string CityName { get; set; }

        public string LogoUrl { get; set; }

        public string CountryName { get; set; }

        public string WorkingTime { get; set; }

        public string WorkingDays { get; set; }

        public string NonWorkingDays { get; set; }

        public int Rating { get; set; }

        public string PromotionContent { get; set; }

        public ICollection<BasicCarPartVm> CarParts { get; set; }
    }
}
