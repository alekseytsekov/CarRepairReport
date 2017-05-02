namespace CarRepairReport.Models.ViewModels.ForumVm
{
    using System;
    using System.Collections.Generic;

    public class ViewPostVm:ViewBindingModel
    {
        public ViewPostVm()
        {
            this.Children = new List<PostVm>();
            //this.Categories = new HashSet<string>();
            //this.Tags = new HashSet<string>();
        }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        //public ICollection<string> Categories { get; set; }
        //public ICollection<string> Tags { get; set; }
        public PostVm Original { get; set; }
        public PostVm Parent { get; set; }
        public ICollection<PostVm> Children { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int Id { get; set; }
        public string WebLink { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
