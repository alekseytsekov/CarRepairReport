namespace CarRepairReport.Models.Models.LanguageModels
{
    using CarRepairReport.Models.Enums;

    public class LanguageValue : BaseModel
    {
        public int Id { get; set; }
        public BelongTo? Type { get; set; }
        public string LangTwoLetterCode { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
