namespace CarRepairReport.Models.Models.UserModels
{
    using CarRepairReport.Models.Models.LanguageModels;

    public class UserSetting : BaseModel
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
    }
}
