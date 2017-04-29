namespace CarRepairReport.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using CarRepairReport.Models.Attributes;


    public class EditUserBm : ViewBindingModel
    {
        [Required]
        [MinLength(1)]
        [RegexValidation("^[a-zA-Z]{2,}$", "system.common.validation.firstname")]
        public string FirstName { get; set; }

        [RegexValidation("^[a-zA-Z]{2,}$", "system.common.validation.lastname")]
        public string LastName { get; set; }

        [RegexValidation("^[a-zA-Z]{2,}$", "system.common.validation.cityname")]
        public string CityName { get; set; }

        [RegexValidation("^[a-zA-Z]{2,}$", "system.common.validation.countryname")]
        public string CountryName { get; set; }

        //[FileSize(1024 * 1024)]
        //[FileTypes("jpg,jpeg")]
        public HttpPostedFileBase Image { get; set; }

        public string ServerPath { get; set; }
    }
}
