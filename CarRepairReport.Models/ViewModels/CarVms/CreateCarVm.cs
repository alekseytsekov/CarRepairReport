namespace CarRepairReport.Models.ViewModels.CarVms
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using CarRepairReport.Models.Enums;

    public class CreateCarVm : ViewBindingModel
    {
        [RegularExpression("[a-zA-Z]{2,}")]
        [Required]
        public string Make { get; set; }

        [RegularExpression("[a-zA-Z]{2,}")]
        [Required]
        public string Model { get; set; }
        
        public string VIN { get; set; }

        public FuelType FuelType { get; set; }

        [Range(0.01d,10d)]
        public decimal EngineSize { get; set; }

        [Range(1,2500)]
        public int EnginePowerKw { get; set; }

        [Range(1,2500)]
        public int EnginePowerHp { get; set; }

        public GearBoxType GearBoxType { get; set; }

        [Range(0,10)]
        public int NumberOfGears { get; set; }

        public DateTime FirstRegistration { get; set; }

        [Range(0,2000000)]
        public int RunningDistanceKm { get; set; }

        [Range(0, 2000000)]
        public int RunningDistanceM { get; set; }
    }
}
