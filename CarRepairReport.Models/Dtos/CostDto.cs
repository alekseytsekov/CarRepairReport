namespace CarRepairReport.Models.Dtos
{
    public class CostDto
    {
        public bool HasInvest { get; set; }

        public string InvestMessage { get; set; }

        public bool HasNewPart { get; set; }

        public string NewPartMessage { get; set; }

        public int Quantity { get; set; }

        public bool HasError { get; set; }

        public string ErrorMessage { get; set; }
    }
}
