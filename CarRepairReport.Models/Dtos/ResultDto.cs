namespace CarRepairReport.Models.Dtos
{
    public class ResultDto
    {
        public ResultDto(string message, bool isSucced = false)
        {
            this.Message = message;
            this.IsSucceed = isSucced;
        }

        public bool IsSucceed { get; set;}

        public string Message { get; set; }
    }
}
