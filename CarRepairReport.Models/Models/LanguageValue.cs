namespace CarRepairReport.Models.Models
{
    public class LanguageValue : BaseModel
    {
        public int Id { get; set; }
        public string LangTwoLetterCode { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
