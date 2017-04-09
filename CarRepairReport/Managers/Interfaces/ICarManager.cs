namespace CarRepairReport.Managers.Interfaces
{
    using CarRepairReport.Models.BindingModels;

    public interface ICarManager
    {
        bool CreateCar(CreateCarBm bm, string appUserId);
    }
}