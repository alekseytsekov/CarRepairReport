namespace CarRepairReport.Models.Models.CarComponents
{
    using System.Collections.Generic;
    using CarRepairReport.Models.Enums;

    public class Gearbox : BaseModel
    {
        public Gearbox()
        {
            this.Cars = new HashSet<Car>();
        }

        public int  Id { get; set; }

        public int NumberOfGears { get; set; }

        public GearBoxType GearBoxType { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
