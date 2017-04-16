namespace CarRepairReport.Models.ViewModels.ServiceVms
{
    using System.ComponentModel.DataAnnotations;

    public class ShortServiceVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Repaired Parts")]
        public int RepairedParts { get; set; }

        public int Rating { get; set; }

        public override string ToString()
        {
            var result = string.Format("{0} {1} {2}", this.Name, this.RepairedParts, this.Rating);

            return result;
        }
    }
}
