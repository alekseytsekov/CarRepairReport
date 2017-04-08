namespace CarRepairReport.Models.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using CarRepairReport.Models.Attributes;

    public class EditUserVm : ViewBindingModel
    {
        public EditUserVm()
        {
            this.Errors = new Dictionary<string, string>();
        }

        [Required]
        [MinLength(1), MaxLength(40)]
        [RegexValidation("^[a-zA-Z]{2,}$", "system.common.validation.firstname")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(1), MaxLength(40)]
        [RegexValidation("^[a-zA-Z]{2,}$", "system.common.validation.lastname")]
        public string LastName { get; set; }

        [Required]
        [MinLength(2), MaxLength(40)]
        [RegexValidation("^[a-zA-Z]{2,}$", "system.common.validation.cityname")]
        public string CityName { get; set; }

        [Required]
        [MinLength(2), MaxLength(40)]
        [RegexValidation("^[a-zA-Z]{2,}$", "system.common.validation.countryname")]
        public string CountryName { get; set; }

        public IDictionary<string, string> Errors { get; set; }
    }
}
