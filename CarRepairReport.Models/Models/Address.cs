namespace CarRepairReport.Models.Models
{
    public class Address : BaseModel
    {
        public int Id { get; set; }

        public AddressType AddressType { get; set; }
        public string StreetName { get; set; }
        public string Neighborhood { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        
    }
}
