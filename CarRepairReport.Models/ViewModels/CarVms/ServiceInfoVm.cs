namespace CarRepairReport.Models.ViewModels.CarVms
{
    using System.Text;

    public class ServiceInfoVm : ViewBindingModel
    {
        public string CarPartSerialNumber { get; set; }

        public string CarPartName { get; set; }

        public string CarPartManufacturerName { get; set; }

        public string CarMake { get; set; }

        public string CarModel { get; set; }

        public string CarYear { get; set; }

        public string CarEngine { get; set; }

        public string CarEngineType { get; set; }

        public string CarGearbox { get; set; }

        public string ServicedBy { get; set; }

        public int Count { get; set; }

        public override string ToString()
        {
            var result = new StringBuilder();

            if (!string.IsNullOrEmpty(this.CarPartSerialNumber))
            {
                result.Append(this.CarPartSerialNumber + ", ");
            }

            if (!string.IsNullOrEmpty(this.CarPartName))
            {
                result.Append(this.CarPartName + ", ");
            }

            if (!string.IsNullOrEmpty(this.CarPartManufacturerName))
            {
                result.Append(this.CarPartManufacturerName + ", ");
            }

            if (!string.IsNullOrEmpty(this.CarMake))
            {
                result.Append(this.CarMake + ", ");
            }

            if (!string.IsNullOrEmpty(this.CarModel))
            {
                result.Append(this.CarModel + ", ");
            }

            if (!string.IsNullOrEmpty(this.CarYear))
            {
                result.Append(this.CarYear + ", ");
            }

            if (!string.IsNullOrEmpty(this.CarEngine))
            {
                result.Append(this.CarEngine + ", ");
            }

            if (!string.IsNullOrEmpty(this.CarGearbox))
            {
                result.Append(this.CarGearbox + ", ");
            }

            if (!string.IsNullOrEmpty(this.ServicedBy))
            {
                result.Append(this.ServicedBy + ", ");
            }

            return result.ToString().TrimEnd().TrimEnd(',');
        }
    }
}
