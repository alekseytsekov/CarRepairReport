namespace CarRepairReport.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using CarRepairReport.Models.BindingModels;
    using System.Web.Http;
    using AutoMapper;
    using CarRepairReport.Models.Dtos;
    using CarRepairReport.Models.ViewModels.CarVms;
    using CarRepairReport.Models.ViewModels.Commons;

    [System.Web.Mvc.Authorize]
    [RoutePrefix("Cost")]
    public class CostController : Controller
    {
        [Route]
        [System.Web.Mvc.HttpPost]
        public JsonResult Cost([Bind(Prefix = "InvestPart")]InvestPartBm bm)
        {
            var result = new CostDto();

            if (!string.IsNullOrWhiteSpace(bm.Name) && bm.Price > 0 && (bm.DistanceTraveled > 0 || bm.MountedOnMi > 0 || bm.MountedOnKm > 0))
            {
                var newInvest = Mapper.Map<InvestPartBm,CreateInvestVm>(bm);

                var isInvestAdded = true;

                if (isInvestAdded)
                {
                    result.HasInvest = isInvestAdded;
                    result.InvestMessage = this.GetMessage(newInvest.Name, isInvestAdded);
                }
                else
                {
                    result.HasInvest = isInvestAdded;
                    result.HasError = !isInvestAdded;
                    result.ErrorMessage = this.GetMessage(newInvest.Name, isInvestAdded);
                }
            }

            if (!string.IsNullOrWhiteSpace(bm.SerialNumber) && !string.IsNullOrWhiteSpace(bm.PartName) && bm.PartPrice > 0)
            {
                var quantity = 1;

                if (bm.Quantity > 1)
                {
                    quantity = bm.Quantity;
                }

                var carPart = Mapper.Map<InvestPartBm, CreateCarPartVm>(bm);

                for (int i = 0; i < quantity; i++)
                {
                    
                    // send to manager  
                }

                var isCarPartAdded = true;

                if (isCarPartAdded)
                {
                    result.HasInvest = isCarPartAdded;
                    result.InvestMessage = this.GetMessage(carPart.PartName, isCarPartAdded, quantity);
                }
                else
                {
                    result.HasInvest = isCarPartAdded;
                    result.HasError = !isCarPartAdded;
                    result.ErrorMessage = this.GetMessage(carPart.PartName, isCarPartAdded);
                }
            }

            return new JsonResult();
        }

        private string GetMessage(string param, bool isAdded, int quantity = 0)
        {
            if (isAdded)
            {
                if (quantity > 1)
                {
                    return string.Format("{0} has been added {1} times.", param, quantity);
                }

                return string.Format("{0} has been added.", param);
            }
            else
            {
                return string.Format("Something goes wrong! Cannot add {0}.", param);
            }
        }
    }
}