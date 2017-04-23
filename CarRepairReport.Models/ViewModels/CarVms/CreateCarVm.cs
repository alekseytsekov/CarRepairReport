namespace CarRepairReport.Models.ViewModels.CarVms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using CarRepairReport.Models.Enums;

    public class CreateCarVm : ViewBindingModel
    {
        public CreateCarVm()
        {
            this.GearBoxValues = new Dictionary<int, string>();
            this.FuelTypeValues = new Dictionary<int, string>();
        }

        [RegularExpression(@"^[a-zA-Z\d]([\sa-zA-Z-\d]+)?$")]
        [Required]
        public string Make { get; set; }

        [RegularExpression(@"^[a-zA-Z\d]([\sa-zA-Z-\d]+)?$")]
        [Required]
        public string Model { get; set; }
        
        public string VIN { get; set; }

        public FuelType FuelType { get; set; }

        public IDictionary<int,string> FuelTypeValues { get; set; }

        [Range(0.01d,10d)]
        public decimal EngineSize { get; set; }
        
        public int EnginePowerKw { get; set; }
        
        public int EnginePowerHp { get; set; }

        public GearBoxType GearBoxType { get; set; }

        public IDictionary<int,string> GearBoxValues { get; set; }

        [Range(0,10)]
        public int NumberOfGears { get; set; }

        [DataType(DataType.Date)]
        public DateTime FirstRegistration { get; set; }
        
        public int RunningDistanceKm { get; set; }
        
        public int RunningDistanceM { get; set; }
    }
}
