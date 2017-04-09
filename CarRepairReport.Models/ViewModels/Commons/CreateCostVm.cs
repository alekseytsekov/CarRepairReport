namespace CarRepairReport.Models.ViewModels.Commons
{
    using System.ComponentModel.DataAnnotations;

    public class CreateCostVm
    {
        [Required]
        [MinLength(2),MaxLength(30)]
        public string Name { get; set; }

        [Range(0.01d,1000000d)]
        public decimal Price { get; set; }

        public int DistanceTraveled { get; set; }

        public int MountedOnKm { get; set; }

        public int MountedOnMi { get; set; }
    }
}
