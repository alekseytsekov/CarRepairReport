namespace CarRepairReport.Models.Models.AddressModels
{
    using System.Collections.Generic;
    using CarRepairReport.Models.Models.UserModels;

    public class Address : BaseModel
    {
        public Address()
        {
            this.Users = new List<User>();
        }
        public int Id { get; set; }
        public AddressType AddressType { get; set; }
        public string StreetName { get; set; }
        public string Neighborhood { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        //public string UserId { get; set; }
        //public virtual User User { get; set; }
        public bool IsPrimary { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
