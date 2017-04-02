namespace CarRepairReport.Models.Models
{
    public class UserSetting : BaseModel
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
    }
}
