namespace CarRepairReport.Models
{
    using System;

    public class BaseModel : IBaseModel
    {
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
