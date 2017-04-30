namespace CarRepairReport.Models.Models.ForumModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using CarRepairReport.Models.Models.CommonModels;
    using CarRepairReport.Models.Models.UserModels;

    public class Post : BaseModel
    {
        public Post()
        {
            this.Tags = new HashSet<Tag>();
            this.Categories = new HashSet<Category>();
        }
        public int Id { get; set; }

        [ForeignKey("Original")]
        public int? OriginalId { get; set; }
        public virtual Post Original { get; set; }

        [ForeignKey("Parent")]
        public int? ParentId { get; set; }
        public virtual Post Parent { get; set; }

        [ForeignKey("Child")]
        public int? ChildId { get; set; }
        public virtual Post Child { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsQuestion { get; set; }

        [ForeignKey("Author")]
        public string AuthorId { get; set; }
        public virtual User Author { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
