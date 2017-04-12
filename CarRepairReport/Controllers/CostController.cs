namespace CarRepairReport.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using CarRepairReport.Models.BindingModels;
    using AutoMapper;
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.Dtos;
    using CarRepairReport.Models.Models.CommonModels;
    using CarRepairReport.Models.ViewModels.CarVms;
    using CarRepairReport.Models.ViewModels.Commons;
    using Microsoft.AspNet.Identity;

    [System.Web.Mvc.Authorize]
    [RoutePrefix("Cost")]
    public class CostController : Controller
    {
        private ICarManager carManager;

        public CostController(ICarManager carManager)
        {
            this.carManager = carManager;
        }
        
        [Route]
        [HttpPost]
        public JsonResult Cost([Bind(Prefix = "InvestPart")]InvestPartBm bm)
        {
            var result = new CostDto();

            var appUserId = this.User.Identity.GetUserId();

            var carId = bm.CarId;

            if (carId < 1)
            {
                //error 
            }

            Cost investmentEntity = null;

            if (!string.IsNullOrWhiteSpace(bm.Name) && bm.Price > 0 && (bm.DistanceTraveled > 0 || bm.MountedOnMi > 0 || bm.MountedOnKm > 0))
            {
                var newInvestment = Mapper.Map<InvestPartBm,CreateInvestmentVm>(bm);

                investmentEntity = this.carManager.AddNewInvestment(newInvestment, carId, appUserId);

                var isInvestAdded = investmentEntity != null;

                if (isInvestAdded)
                {
                    result.HasInvest = isInvestAdded;
                    result.InvestMessage = this.GetMessage(newInvestment.Name, isInvestAdded);
                }
                else
                {
                    result.HasInvest = isInvestAdded;
                    result.HasError = !isInvestAdded;
                    result.ErrorMessage = this.GetMessage(newInvestment.Name, isInvestAdded);
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

                var isCarPartAdded = true;

                for (int i = 0; i < quantity; i++)
                {
                    isCarPartAdded = this.carManager.AddReplacedPart(carPart, carId, appUserId, investmentEntity);

                    if (!isCarPartAdded)
                    {
                        break;
                    }
                }
                
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