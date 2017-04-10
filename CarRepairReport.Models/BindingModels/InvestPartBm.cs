namespace CarRepairReport.Models.BindingModels
{

    public class InvestPartBm
    {
        public string Name { get; set; }
        
        public decimal Price { get; set; }

        public int DistanceTraveled { get; set; }

        public int MountedOnKm { get; set; }

        public int MountedOnMi { get; set; }

        public string SerialNumber { get; set; }
        
        public string PartName { get; set; }
        
        public decimal PartPrice { get; set; }
        
        public int Quantity { get; set; }

        public string ManufacturerName { get; set; }
    }
}
