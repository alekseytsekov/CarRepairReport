namespace CarRepairReport.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Linq;
    using CarRepairReport.Models.BindingModels.ForumBms;
    using CarRepairReport.Models.Models.CommonModels;
    using CarRepairReport.Models.Models.ForumModels;

    public interface IForumService
    {
        IQueryable<Post> GetPosts();
        IEnumerable<Category> GetCategories();
        IEnumerable<Tag> GetTags();
        Category GetCategoryBySystemName(string categorySystemName);
        bool AddPost(Post post);
        Post GetPostByTitle(string title);
        Post GetPostById(int id);
    }
}
