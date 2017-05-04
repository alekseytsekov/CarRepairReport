namespace CarRepairReport.Models.ViewModels.UserVms
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using CarRepairReport.Models.BindingModels;
    using CarRepairReport.Models.Dtos;

    public class CreateCarServiceVm : ViewBindingModel, IImageUploader
    {
        [Required]
        [MinLength(2), MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MinLength(2), MaxLength(500)]
        public string StreetName { get; set; }

        [Required]
        [MinLength(2), MaxLength(100)]
        public string City { get; set; }

        [Required]
        [MinLength(2), MaxLength(100)]
        public string Country { get; set; }

        //public byte[] Logo { get; set; }

        [Display(Name = "From:")]
        [Required]
        [RegularExpression("^(2[0-3]|1[0-9]|[0-9]|0[0-9]):([0-5][0-9])$",ErrorMessage = "Invalid time format!")]
        public string StartWorkingTime { get; set; }

        [Display(Name = "To:")]
        [Required]
        [RegularExpression("^(2[0-3]|1[0-9]|[0-9]|0[0-9]):([0-5][0-9])$", ErrorMessage = "Invalid time format!")]
        public string EndWorkingTime { get; set; }

        [Required]
        [MinLength(2), MaxLength(2100)]
        public string Description { get; set; }

        public HttpPostedFileBase Image { get; set; }
        public string ServerPath { get; set; }

        public CheckBoxDto[] WorkingDays { get; set; }

        public CheckBoxDto[] NonWorkingDays { get; set; }
    }
}
