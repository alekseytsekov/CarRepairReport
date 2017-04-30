namespace CarRepairReport.Models.Models.CommonModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using CarRepairReport.Models.Enums;
    using CarRepairReport.Models.Models.ForumModels;

    public class Tag : BaseModel
    {
        public Tag()
        {
            this.Posts = new HashSet<Post>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public BelongTo Type { get;set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
