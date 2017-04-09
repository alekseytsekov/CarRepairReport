namespace CarRepairReport.Services.Interfaces
{
    using CarRepairReport.Models.Models.CarComponents;

    public interface ICarService
    {
        bool AddCar(Car car, string appUserId);
        Car GetById(int carId);
        bool RemoveCar(string appUserId, int id);
    }
}
