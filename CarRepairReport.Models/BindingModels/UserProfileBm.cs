namespace CarRepairReport.Models.BindingModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using CarRepairReport.Models.Attributes;

    public class UserProfileBm : ViewBindingModel
    {
        
        [RegexValidation("^[a-zA-Zа-яА-Я]{2,}$", "common.error")]
        public string FirstName { get; set; }
        
        [RegexValidation("^[a-zA-Zа-яА-Я]{2,}$", "common.error2")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public string ImageUrl { get; set; }

        public string StreetName { get; set; }

        public string Neighborhood { get; set; }

        [Required]
        [MinLength(2), MaxLength(30)]
        [RegexValidation("^[a-zA-Z]{2,}$", "asd")]
        public string CityName { get; set; }

        [Required]
        [MinLength(2), MaxLength(30)]
        [RegexValidation("^[a-zA-Z]{2,}$", "asd")]
        public string CountryName { get; set; }
    }
}
