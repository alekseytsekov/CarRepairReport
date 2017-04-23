namespace CarRepairReport.Models.Models.CommonModels
{
    public class ErrorLog : BaseModel
    {
        public int Id { get; set; }

        public string ErrorMessage { get; set; }

        public string StackTrace { get; set; }
    }
}
