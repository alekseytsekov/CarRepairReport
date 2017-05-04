namespace CarRepairReport.Models.BindingModels.ForumBms
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreatePostBm
    {
        
        [RegularExpression("^[a-zA-Z0-9?\\s,]+$", ErrorMessage = "Invalid Title!")]
        [MinLength(3), MaxLength(100)]
        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(3), MaxLength(10000)]
        public string Content { get; set; }

        public string Tags { get; set; }

        [Required]
        [MinLength(1), MaxLength(100)]
        public string Category { get; set; }
    }
}
