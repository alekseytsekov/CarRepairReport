namespace CarRepairReport.Models.ViewModels.UserVms
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using CarRepairReport.Models.Attributes;
    using CarRepairReport.Models.ViewModels.CarVms;

    public class UserProfileVm : ViewBindingModel
    {
        public UserProfileVm()
        {
            this.Errors = new Dictionary<string, string>();
            this.Cars = new List<SimpleCarVm>();
        }
        
        // премахнато поради ЗЗЛД - закон за защита на личните данни 
        //[DataType(DataType.Date)]
        //public DateTime Birthday { get; set; }

        [Required]
        [MinLength(2), MaxLength(30)]
        [RegularExpression("^[a-zA-Z]{2,}$")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2), MaxLength(30)]
        [RegularExpression("^[a-zA-Z]{2,}$")]
        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        //public string StreetName { get; set; }

        [Required]
        [MinLength(2), MaxLength(30)]
        [RegexValidation("^[a-zA-Z]{2,}$","")]
        public string CityName { get; set; }

        [Required]
        [MinLength(2), MaxLength(30)]
        [RegexValidation("^[a-zA-Z]{2,}$", "")]
        public string CountryName { get; set; }

        public bool IsVehicleServiceOwner { get; set; }

        public ICollection<SimpleCarVm> Cars { get; set; }

        public IDictionary<string, string> Errors { get; set; }
    }
}
