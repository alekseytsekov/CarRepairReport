namespace CarRepairReport.Models.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Address : BaseModel
    {
        public int Id { get; set; }
        
        public AddressType AddressType { get; set; }
        public string StreetName { get; set; }
        public string Neighborhood { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
