namespace CarRepairReport.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Helpers;
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
    public class CostController : BaseController
    {
        private ICarManager carManager;

        public CostController(ICarManager carManager, IMyUserManager myUserManager, ILanguageManager languageManager) : base(myUserManager, languageManager)
        {
            this.carManager = carManager;
        }

        [Route]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult Cost([Bind(Prefix = "InvestPart")]InvestPartBm bm)
        {
            var result = new CostDto();
            
            var carId = bm.CarId;

            if (carId < 1)
            {
                this.Response.StatusCode = 400;
                return new JsonResult() {Data = new CostDto() {ErrorMessage = "Cannot process request!"} };
            }

            Cost investmentEntity = null;

            if (!string.IsNullOrWhiteSpace(bm.Name) && bm.Price > 0)// && (bm.DistanceTraveled > 0 || bm.MountedOnMi > 0 || bm.MountedOnKm > 0))
            {
                var newInvestment = Mapper.Map<InvestPartBm,CreateInvestmentVm>(bm);

                investmentEntity = this.carManager.AddNewInvestment(newInvestment, carId, this.GetAppUserId);

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
            else if((string.IsNullOrWhiteSpace(bm.Name) && bm.Price > 0) || (!string.IsNullOrWhiteSpace(bm.Name) && bm.Price <= 0) || (string.IsNullOrWhiteSpace(bm.Name) && bm.Price < 0))
            {
                result.HasError = true;
                result.HasInvest = false;
                result.InvestMessage = string.Format("Cost/Investment contain invalid data!\r\nCost/Investment name: {0}\r\nCost/Investment Price: {1}", bm.Name, bm.Price);
            }

            if (!string.IsNullOrWhiteSpace(bm.PartName) && bm.PartPrice > 0)
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
                    isCarPartAdded = this.carManager.AddReplacedPart(carPart, carId, this.GetAppUserId);

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
                    this.Response.StatusCode = 400;

                    result.HasInvest = isCarPartAdded;
                    result.HasError = !isCarPartAdded;
                    result.ErrorMessage = this.GetMessage(carPart.PartName, isCarPartAdded);

                    return new JsonResult() {Data = result};
                }
            }
            else if ((string.IsNullOrWhiteSpace(bm.PartName) && bm.PartPrice > 0) || (!string.IsNullOrWhiteSpace(bm.PartName) && bm.PartPrice <= 0) || (string.IsNullOrWhiteSpace(bm.PartName) && bm.PartPrice < 0))
            {
                result.NewPartMessage = string.Format("New Part contain invalid data!\r\nPart name: {0}\r\nPart Price: {1}", bm.PartName, bm.PartPrice);
            }
            
            return new JsonResult() { Data = result };
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