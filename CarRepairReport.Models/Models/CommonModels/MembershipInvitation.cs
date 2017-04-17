namespace CarRepairReport.Models.Models.CommonModels
{
    using System.ComponentModel.DataAnnotations;

    public class MembershipInvitation : BaseModel
    {
        public int Id { get; set; }

        [Range(1,int.MaxValue)]
        public int VehicleServiceId { get; set; }

        [Required]
        [MinLength(3),MaxLength(100)]
        public string MemberEmail { get; set; }

        public bool IsAccepted { get; set; }
    }
}
