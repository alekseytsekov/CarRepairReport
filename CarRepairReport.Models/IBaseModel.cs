namespace CarRepairReport.Models
{
    using System;

    public interface IBaseModel
    {
        DateTime CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }

        bool IsDeleted { get; set; }
    }
}
