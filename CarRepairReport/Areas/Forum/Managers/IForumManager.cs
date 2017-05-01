namespace CarRepairReport.Areas.Forum.Managers
{
    using System.Collections.Generic;
    using System.Web;
    using CarRepairReport.Models.BindingModels.ForumBms;
    using CarRepairReport.Models.ViewModels.ForumVm;

    public interface IForumManager
    {
        void SetFilter(HttpSessionStateBase httpContextSession, ForumFilterBm bm);
        PostWrapperVm GetPosts(HttpSessionStateBase session);
        ICollection<string> GetCategories(string language);
        bool CreatePost(CreatePostBm bm, string appUserId, string languageCode);
        ViewPostVm GetPost(string title);
        string CreateAnswer(PostAnswerBm bm, string appUserId);
        ViewPostVm GetPostById(int id);
    }
}
