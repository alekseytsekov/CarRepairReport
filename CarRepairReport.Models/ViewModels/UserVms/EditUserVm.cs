namespace CarRepairReport.Models.ViewModels.UserVms
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
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

        //[FileSize(1024*1024)]
        //[FileTypes("jpg,jpeg")]
        public HttpPostedFileBase Image { get; set; }

        public IDictionary<string, string> Errors { get; set; }
    }
}
