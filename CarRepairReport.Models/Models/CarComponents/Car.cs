namespace CarRepairReport.Models.Models.CarComponents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using CarRepairReport.Models.Models.UserModels;

    public class Car : BaseModel
    {
        public Car()
        {
            this.CarParts = new List<CarPart>();
        }

        public int Id { get; set; }

        public string Model { get; set; }

        public string VIN { get; set; }

        public string Make { get; set; }

        public int GearboxId { get; set; }

        public Gearbox Gearbox { get; set; }

        public int EngineId { get; set; }

        public virtual Engine Engine { get; set; }
        
        public DateTime FirstRegistration { get; set; }

        public int RunningDistance { get; set; }

        [ForeignKey("Owner")]
        public string OwnerId { get; set; }
        
        public virtual User Owner { get; set; }

        public virtual ICollection<CarPart> CarParts { get; set; }
    }
}
