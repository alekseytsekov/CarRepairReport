namespace CarRepairReport.Models.ViewModels.Commons
{
    using System.ComponentModel.DataAnnotations;

    public class CreateInvestmentVm
    {
        [Required]
        [MinLength(2),MaxLength(30)]
        public string Name { get; set; }

        [Range(0.01d,1000000d)]
        public decimal Price { get; set; }
        
    }
}
