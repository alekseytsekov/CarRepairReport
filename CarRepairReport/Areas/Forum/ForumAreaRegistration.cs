namespace CarRepairReport.Areas.Forum
{
    using System.Web.Mvc;

    public class ForumAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Forum";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapMvcAttributeRoutes();

            //context.MapRoute(
            //    "Forum_default",
            //    "Forum/{id}/post",
            //    new { action = "Posts", id = UrlParameter.Optional }
            //);

            context.MapRoute(
                "Forum_default",
                "Forum/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}