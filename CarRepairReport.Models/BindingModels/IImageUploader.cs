namespace CarRepairReport.Models.BindingModels
{
    using System.Web;

    public interface IImageUploader
    {
        HttpPostedFileBase Image { get; set; }

        string ServerPath { get; set; }
    }
}
