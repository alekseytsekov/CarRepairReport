namespace CarRepairReport.Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : BaseModel
    {
        public User()
        {
            this.Addresses = new HashSet<Address>();
        }

        [Key]
        public string Id { get; set; }

        // // премахнато поради ЗЗЛД - закон за защита на личните данни 
        //public DateTime Birthday { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public int UserSettingId { get; set; }

        public virtual UserSetting UserSetting { get; set; }
        
        public virtual ICollection<Address> Addresses { get; set; }

    }
}
