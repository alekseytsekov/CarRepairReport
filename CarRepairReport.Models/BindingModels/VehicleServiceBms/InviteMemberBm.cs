namespace CarRepairReport.Models.BindingModels.VehicleServiceBms
{
    using System.ComponentModel.DataAnnotations;

    public class InviteMemberBm
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^(?:[a-z]+|[A-Z]+)(?:[a-zA-Z\d\.\-_]*)(?:[a-z\d]+|[A-Z\d]+)@(?:[a-z]+|[A-Z]+)(?:[a-zA-Z\d\.\-_]*)(?:[a-z]+|[A-Z]+)\.(?:[a-z]+|[A-Z]+)(?:[a-zA-Z\d]*)(?:[a-z]+|[A-Z]+)$", ErrorMessage = "Invalid email!")]
        public string MemberEmail { get; set; }
    }
}
