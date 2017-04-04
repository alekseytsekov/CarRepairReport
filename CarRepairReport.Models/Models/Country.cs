namespace CarRepairReport.Models.Models
{
    using System.Collections.Generic;

    public class Country : BaseModel
    {
        public Country()
        {
            this.Cities = new HashSet<City>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string CountryCode { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}