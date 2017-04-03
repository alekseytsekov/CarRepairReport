namespace CarRepairReport.Models.BindingModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using CarRepairReport.Models.Attributes;

    public class UserProfileBm
    {
        
        //[RegularExpression("^[a-zA-Z]{2,}$",ErrorMessage = "Invalid data name")]
        [RegexValidation("^[a-zA-Z]{2,}$","common.error")]
        public string FirstName { get; set; }

        [Required]
        [RegexValidation("^[a-zA-Z]{2,}$", "common.error2")]
        //[RegularExpression("^[a-zA-Z]{2,}$")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public string ImageUrl { get; set; }
    }
}
