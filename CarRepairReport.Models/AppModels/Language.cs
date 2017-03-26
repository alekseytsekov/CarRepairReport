namespace CarRepairReport.Models.AppModels
{
    public class Language : BaseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TwoLetterCode { get; set; }
    }
}
