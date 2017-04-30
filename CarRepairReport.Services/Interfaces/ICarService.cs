namespace CarRepairReport.Services.Interfaces
{
    using System.Collections.Generic;
    using CarRepairReport.Models.Models.CarComponents;
    using CarRepairReport.Models.Models.CommonModels;

    public interface ICarService : IService
    {
        bool AddCar(Car car, string appUserId);
        Car GetById(int carId);
        bool RemoveCar(string appUserId, int id);
        IEnumerable<Car> AllUserCars(string userId);
        Manufacturer GetCarPartManufacturerByName(string carPartManufacturerName);
        bool AddManufacturer(Manufacturer entityManufacturer);
        bool AddCarPart(CarPart newPart);
        bool AddInvestment(Cost newInvestment);
        CarPart GetCarPartById(int id);

        IEnumerable<CarPart> LatestCarParts(int count = 100);
    }
}
