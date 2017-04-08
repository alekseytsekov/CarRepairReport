namespace CarRepairReport.Models.Models.CarComponents
{
    using System.Collections.Generic;

    public class Manufacturer : BaseModel
    {
        public Manufacturer()
        {
            this.CarParts = new HashSet<CarPart>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<CarPart> CarParts { get; set; }
    }
}
