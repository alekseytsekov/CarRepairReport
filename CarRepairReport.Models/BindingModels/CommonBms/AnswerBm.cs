namespace CarRepairReport.Models.BindingModels.CommonBms
{
    using System.ComponentModel.DataAnnotations;

    public class AnswerBm
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        public bool IsAccepted { get; set; }

        public string Message { get; set; }
    }
}
