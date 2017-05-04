namespace CarRepairReport.Models.ViewModels.ForumVm
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreatePostVm : ViewBindingModel
    {
        public CreatePostVm()
        {
            this.Categories = new HashSet<string>();
        }

        [RegularExpression("^[a-zA-Z0-9?\\s,]+$", ErrorMessage = "Invalid Title!")]
        [Required]
        [MinLength(3), MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MinLength(3), MaxLength(10000)]
        public string Content { get; set; }
        public string Tags { get; set; }

        [Required]
        [MinLength(1), MaxLength(100)]
        public string Category { get; set; }
        public ICollection<string> Categories { get; set; }

    }
}
