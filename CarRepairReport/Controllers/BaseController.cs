namespace CarRepairReport.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.ViewModels.Commons;
    using Microsoft.AspNet.Identity;

    public abstract class BaseController : Controller
    {
        protected IMyUserManager myUserManager;

        protected BaseController(IMyUserManager myUserManager)
        {
            this.myUserManager = myUserManager;
        }

        //public ICollection<MembershipInvitationVm> GetInvitations()
        //{
        //    var email = this.User.Identity.GetUserName();

        //    ICollection<MembershipInvitationVm> invitations = this.myUserManager.GetInvitations(email);

        //    return invitations;
        //}

        public ActionResult ShowInvitations()
        {
            var email = this.User.Identity.GetUserName();

            ICollection<MembershipInvitationVm> invitations = this.myUserManager.GetInvitations(email);

            return this.PartialView("_Invitations", invitations);
        }
    }
}