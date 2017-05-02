namespace CarRepairReport.Models.Dtos
{
    using CarRepairReport.Models.Enums;

    public class LanguageDto
    {
        public string LanguageCode { get; set; }
        public BelongTo? Type { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
