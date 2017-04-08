namespace CarRepairReport.Models.Models.CarComponents
{
    using CarRepairReport.Models.Enums;

    public class Gearbox : BaseModel
    {
        public int  Id { get; set; }

        public int NumberOfGears { get; set; }

        public GearBoxType GearBoxType { get; set; }
    }
}
