namespace CarRepairReport.Models.ViewModels.CarVms
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CarRepairReport.Models.Models.CarComponents;
    using CarRepairReport.Models.Models.CommonModels;

    public class FullCarVm
    {
        public int Id { get; set; }

        public string CarNickname { get; set; }

        public string Model { get; set; }

        public string VIN { get; set; }

        public string Make { get; set; }

        public string Gearbox { get; set; }

        public string Engine { get; set; }

        public DateTime FirstRegistration { get; set; }

        public int RunningDistance { get; set; }
        
        public virtual ICollection<CarPartVm> CarParts { get; set; }

        public virtual ICollection<CostVm> Costs { get; set; }

        public decimal TotalSpendOnCar { get; set; }

        public decimal SpendOnCarParts { get; set; }

        public decimal SpendOnCosts { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(this.CarNickname))
            {
                sb.Append(this.CarNickname + ", ");
            }

            if (!string.IsNullOrWhiteSpace(this.VIN))
            {
                sb.Append(this.VIN + ", ");
            }

            sb.Append(this.Model + ", ");
            sb.Append(this.Make + ", ");
            sb.Append(this.FirstRegistration.ToString("D"));

            return sb.ToString();
        }
    }
}
