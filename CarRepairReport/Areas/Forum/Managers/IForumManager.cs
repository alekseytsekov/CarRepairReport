namespace CarRepairReport.Areas.Forum.Managers
{
    using System.Collections.Generic;
    using System.Web;
    using CarRepairReport.Models.BindingModels.ForumBms;
    using CarRepairReport.Models.ViewModels.ForumVm;

    public interface IForumManager
    {
        void SetFilter(HttpSessionStateBase httpContextSession, ForumFilterBm bm);
        PostWrapperVm GetPosts(HttpSessionStateBase session, string languageCode);
        ICollection<string> GetCategories(string language);
        bool CreatePost(CreatePostBm bm, string appUserId, string languageCode);
        ViewPostVm GetPost(string title, string languageCode);
        string CreateAnswer(PostAnswerBm bm, string appUserId);
        ViewPostVm GetPostById(int id);
        CategoryTagVm GetCategoryTagVms(string language);
        string[] GetCategorySystemNameByString(IEnumerable<string> input);
        string GetCategorySystemNameById(int id);
        void SetPage(int page);
        void SetToPage(int page);
    }
}
