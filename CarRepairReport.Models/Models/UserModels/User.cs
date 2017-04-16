namespace CarRepairReport.Models.Models.UserModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using CarRepairReport.Models.Models.AddressModels;
    using CarRepairReport.Models.Models.CarComponents;
    using CarRepairReport.Models.Models.CommonModels;

    public class User : BaseModel
    {
        public User()
        {
            this.Addresses = new List<Address>();
            this.Cars = new HashSet<Car>();
            this.ServiceRatings = new HashSet<ServiceRating>();
        }

        [Key]
        public string Id { get; set; }

        // //remove some fields to prevent personal data protection directive conflict --- премахнато поради ЗЗЛД - закон за защита на личните данни 
        //public DateTime Birthday { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public int UserSettingId { get; set; }

        public virtual UserSetting UserSetting { get; set; }

        public int? VehicleServiceId { get; set; }
        
        public virtual VehicleService VehicleService { get; set; }

        public bool IsVehicleServiceOwner { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public virtual ICollection<Car> Cars { get; set; }

        public virtual ICollection<ServiceRating> ServiceRatings { get; set; }

        public decimal GetTotalSpendOnCars()
        {
            var totalSpent = 0m;

            foreach (var car in this.Cars)
            {
                foreach (var part in car.CarParts)
                {
                    totalSpent += part.Price;
                }
            }

            return totalSpent;
        }

    }
}
