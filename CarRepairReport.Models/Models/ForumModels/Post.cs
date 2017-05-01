namespace CarRepairReport.Models.Models.ForumModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.RegularExpressions;
    using CarRepairReport.Models.Models.CommonModels;
    using CarRepairReport.Models.Models.UserModels;

    public class Post : BaseModel
    {
        public Post()
        {
            this.Tags = new HashSet<Tag>();
            this.Categories = new HashSet<Category>();
            this.Children = new List<Post>();
        }
        public int Id { get; set; }

        [ForeignKey("Original")]
        public int? OriginalId { get; set; }
        public virtual Post Original { get; set; }

        [ForeignKey("Parent")]
        public int? ParentId { get; set; }
        public virtual Post Parent { get; set; }
        
        public virtual ICollection<Post> Children { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsQuestion { get; set; }

        [ForeignKey("Author")]
        public string AuthorId { get; set; }
        public virtual User Author { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

        public string WebLink()
        {
            var result = Regex.Replace(this.Title, "\\s+", "-");
            result = Regex.Replace(this.Title, @"\?", "");

            if (result[0] == '$' && result.Length > 1)
            {
                result = result.Substring(1);
            }

            return result;
        }
    }
}
