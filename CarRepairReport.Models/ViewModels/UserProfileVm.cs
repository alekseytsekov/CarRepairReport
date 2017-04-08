namespace CarRepairReport.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserProfileVm : ViewBindingModel
    {
        public UserProfileVm()
        {
            this.Errors = new Dictionary<string, string>();
        }
        
        // премахнато поради ЗЗЛД - закон за защита на личните данни 
        //[DataType(DataType.Date)]
        //public DateTime Birthday { get; set; }

        
        [RegularExpression("^[a-zA-Z]{2,}$")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z]{2,}$")]
        public string LastName { get; set; }

        public string ImageUrl { get; set; }
        
        //public string StreetName { get; set; }
        
        public string City { get; set; }

        public string Country { get; set; }

        public IDictionary<string, string> Errors { get; set; }
    }
}
