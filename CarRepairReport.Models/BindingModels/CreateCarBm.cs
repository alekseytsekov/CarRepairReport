namespace CarRepairReport.Models.BindingModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using CarRepairReport.Models.Enums;

    public class CreateCarBm
    {
        [RegularExpression("[a-zA-Z]{2,}\\s?[a-zA-Z-]{2,}")]
        [Required]
        public string Make { get; set; }

        [RegularExpression("[a-zA-Z]{2,}\\s?[a-zA-Z]{2,}")]
        [Required]
        public string Model { get; set; }

        [MinLength(17), MaxLength(17)]
        public string VIN { get; set; }

        public FuelType FuelType { get; set; }

        [Range(0.01d, 10d)]
        public decimal EngineSize { get; set; }

        [Range(0,2500)]
        public int EnginePowerKw { get; set; }

        [Range(0,2500)]
        public int EnginePowerHp { get; set; }

        public GearBoxType GearBoxType { get; set; }

        [Range(0, 12)]
        public int NumberOfGears { get; set; }

        [DataType(DataType.Date)]
        public DateTime FirstRegistration { get; set; }

        [Range(0, 2000000)]
        public int RunningDistanceKm { get; set; }

        [Range(0, 2000000)]
        public int RunningDistanceM { get; set; }
    }
}
