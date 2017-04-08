namespace CarRepairReport.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using CarRepairReport.Models.Attributes;

    public class UserProfileVm : ViewBindingModel
    {
        public UserProfileVm()
        {
            this.Errors = new Dictionary<string, string>();
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

        public IDictionary<string, string> Errors { get; set; }
    }
}
