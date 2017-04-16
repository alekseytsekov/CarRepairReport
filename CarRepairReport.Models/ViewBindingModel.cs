namespace CarRepairReport.Models
{
    using CarRepairReport.Models.Dtos;

    public abstract class ViewBindingModel
    {
        public string LanguageCode { get; set; }

        public ResultDto Result { get; set; }
    }
}
