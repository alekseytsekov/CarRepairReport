namespace CarRepairReport.Services.Interfaces
{
    using CarRepairReport.Models.Models.CarComponents;

    public interface ICarService
    {
        bool AddCar(Car car, string appUserId);
    }
}
