namespace CarRepairReport.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarRepairReport.Data;
    using CarRepairReport.Models.BindingModels.ForumBms;
    using CarRepairReport.Models.Models.CommonModels;
    using CarRepairReport.Models.Models.ForumModels;
    using CarRepairReport.Services.Interfaces;

    public class ForumService : Service, IForumService
    {
        public ForumService(ICarRepairReportData context) : base(context)
        {
        }

        public IQueryable<Post> GetPosts()
        {
            return this.context.Posts.GetAll();
        }

        public IEnumerable<Category> GetCategories()
        {
            return this.context.Categories.All();
        }

        public IEnumerable<Tag> GetTags()
        {
            return this.context.Tags.All();
        }

        public Category GetCategoryBySystemName(string categorySystemName)
        {
            return this.context.Categories.FirstOrDefault(x => x.Name == categorySystemName);
        }

        public bool AddPost(Post post)
        {
            try
            {
                this.context.Posts.Add(post);

                this.context.Commit();
            }
            catch (Exception ex)
            {
                return this.LogError(ex);
            }

            return true;
        }

        public Post GetPostByTitle(string title)
        {
            return this.context.Posts.FirstOrDefault(x => x.Title == title);
        }

        public Post GetPostById(int id)
        {
            return this.context.Posts.FirstOrDefault(x => x.Id == id);
        }
    }
}
