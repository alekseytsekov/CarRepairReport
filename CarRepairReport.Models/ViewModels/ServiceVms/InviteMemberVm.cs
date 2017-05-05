namespace CarRepairReport.Models.ViewModels.ServiceVms
{
    using System.ComponentModel.DataAnnotations;

    public class InviteMemberVm: ViewBindingModel
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^(?:[a-z]+|[A-Z]+)(?:[a-zA-Z\d\.\-_]*)(?:[a-z]+|[A-Z]+)@(?:[a-z]+|[A-Z]+)(?:[a-zA-Z\d\.\-_]*)(?:[a-z]+|[A-Z]+)\.(?:[a-z]+|[A-Z]+)(?:[a-zA-Z\d]*)(?:[a-z]+|[A-Z]+)$", ErrorMessage = "Invalid email!")]
        public string MemberEmail { get; set; }
    }
}
