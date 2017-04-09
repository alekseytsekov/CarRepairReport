namespace CarRepairReport.Managers.Interfaces
{
    using CarRepairReport.Models.BindingModels;
    using CarRepairReport.Models.Models.CarComponents;
    using CarRepairReport.Models.ViewModels.CarVms;

    public interface ICarManager
    {
        bool CreateCar(CreateCarBm bm, string appUserId);
        SimpleCarVm MapToSimpleVm(Car car);
        SimpleCarVm GetSimpleVm(string appUserId, int carId);
        bool RemoveCarFromUser(string appUserId, int id);
    }
}