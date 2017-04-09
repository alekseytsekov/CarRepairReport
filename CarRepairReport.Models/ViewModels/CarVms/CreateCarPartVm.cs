namespace CarRepairReport.Models.ViewModels.CarVms
{
    using System.ComponentModel.DataAnnotations;

    public class CreateCarPartVm
    {
        public string SerialNumber { get; set; }

        //[Required]
        //[MinLength(2), MaxLength(50)]
        public string Name { get; set; }

        //[Range(0.01d, 1000000d)]
        public decimal Price { get; set; }

        //[Range(0, 100)]
        public int Quantity { get; set; }

        public string ManufacturerName { get; set; }
    }
}
