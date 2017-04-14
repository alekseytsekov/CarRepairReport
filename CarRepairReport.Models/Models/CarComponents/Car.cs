namespace CarRepairReport.Models.Models.CarComponents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;
    using CarRepairReport.Models.Models.CommonModels;
    using CarRepairReport.Models.Models.UserModels;

    public class Car : BaseModel
    {
        public Car()
        {
            this.CarParts = new List<CarPart>();
            this.Costs = new HashSet<Cost>();
        }

        public int Id { get; set; }

        public string CarNickname { get; set; }

        public string Model { get; set; }

        public string VIN { get; set; }

        public string Make { get; set; }

        public int GearboxId { get; set; }

        public Gearbox Gearbox { get; set; }

        public int EngineId { get; set; }

        public virtual Engine Engine { get; set; }
        
        public DateTime FirstRegistration { get; set; }

        public int RunningDistance { get; set; }

        [ForeignKey("Owner")]
        public string OwnerId { get; set; }
        
        public virtual User Owner { get; set; }

        public virtual ICollection<CarPart> CarParts { get; set; }

        public virtual ICollection<Cost> Costs { get; set; }

        public decimal TotalSpendOnCar()
        {
            var money = this.SpendOnCarParts() + this.SpendOnCosts();
            
            return money;
        }

        public decimal SpendOnCarParts()
        {
            var money = 0m;

            foreach (var carPart in this.CarParts)
            {
                money += carPart.Price;
            }

            return money;
        }

        public decimal SpendOnCosts()
        {
            var money = 0m;

            foreach (var cost in this.Costs)
            {
                money += cost.Price;
            }

            return money;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(this.CarNickname))
            {
                sb.Append(this.CarNickname + ", ");
            }

            sb.Append(this.Model + ", ");
            sb.Append(this.FirstRegistration.Year);

            return sb.ToString();
        }
    }
}
