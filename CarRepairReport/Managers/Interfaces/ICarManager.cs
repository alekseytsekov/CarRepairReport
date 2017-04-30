namespace CarRepairReport.Managers.Interfaces
{
    using System.Collections.Generic;
    using CarRepairReport.Models.BindingModels;
    using CarRepairReport.Models.Models.CarComponents;
    using CarRepairReport.Models.Models.CommonModels;
    using CarRepairReport.Models.ViewModels.CarVms;
    using CarRepairReport.Models.ViewModels.Commons;

    public interface ICarManager
    {
        bool CreateCar(CreateCarBm bm, string appUserId);
        SimpleCarVm MapToSimpleVm(Car car);
        SimpleCarVm GetSimpleVm(string appUserId, int carId);
        bool RemoveCarFromUser(string appUserId, int id);
        IEnumerable<string> GetVehicleServiceNames();
        IDictionary<int, string> GetCarNames(string userId);
        bool AddReplacedPart(CreateCarPartVm carPart, int carId, string appUserId);
        Cost AddNewInvestment(CreateInvestmentVm newInvestment, int carId, string appUserId);
        FullCarVm GetFullCarInfo(int carId, string appUserId);
        IEnumerable<ServiceInfoVm> LastServicedCarParts();
    }
}