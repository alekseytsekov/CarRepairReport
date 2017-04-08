namespace CarRepairReport.Models.Models.LanguageModels
{
    public class Language : BaseModel
    {
        public int Id { get; set; }

        public bool IsSystemLanguage { get; set; }

        public string Name { get; set; }

        public string TwoLetterCode { get; set; }
    }
}
