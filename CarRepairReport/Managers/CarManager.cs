namespace CarRepairReport.Managers
{
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.BindingModels;
    using CarRepairReport.Models.Models.CarComponents;
    using CarRepairReport.Models.ViewModels.CarVms;
    using CarRepairReport.Services.Interfaces;

    public class CarManager : ICarManager
    {
        private const double OneKw = 1.34d;
        private const double OneMile = 1.609d;

        private ICarService carService;

        public CarManager(ICarService carService)
        {
            this.carService = carService;
        }

        public bool CreateCar(CreateCarBm bm, string appUserId)
        {
            int enginePower = this.EnginePowerToHp(bm.EnginePowerHp, bm.EnginePowerKw);

            if (enginePower == 0)
            {
                return false;
            }

            int runningDistance = this.RunningDistanceToKm(bm.RunningDistanceKm, bm.RunningDistanceM);

            if (runningDistance == 0)
            {
                return false;
            }
            
            Engine engine = new Engine()
            {
                FuelType = bm.FuelType,
                EngineSize = bm.EngineSize,
                EnginePower = enginePower
            };
            
            Gearbox gearbox = new Gearbox()
            {
                GearBoxType = bm.GearBoxType,
                NumberOfGears = bm.NumberOfGears
            };

            Car car = new Car()
            {
                Make = bm.Make,
                Model = bm.Model,
                FirstRegistration = bm.FirstRegistration,
                VIN = bm.VIN,
                RunningDistance = runningDistance,
                Engine = engine,
                Gearbox = gearbox
            };

            bool isAdded = this.carService.AddCar(car, appUserId);

            return true;
        }

        public SimpleCarVm MapToSimpleVm(Car car)
        {
            var vmCar = new SimpleCarVm()
            {
                Id = car.Id,
                Model = car.Model,
                Make = car.Make,
                FuelType = car.Engine.FuelType,
                RunningDistance = car.RunningDistance,
                NumberOfServices = car.CarParts.Count,
                TotalSpent = car.SpendOnCar()
            };

            return vmCar;
        }

        public SimpleCarVm GetSimpleVm(string appUserId, int carId)
        {
            var car = this.carService.GetById(carId);

            if (car == null)
            {
                return null;
            }

            var carBelongToUser = car.Owner.ApplicationUserId == appUserId;

            if (!carBelongToUser)
            {
                return null;
            }

            return this.MapToSimpleVm(car);
        }

        public bool RemoveCarFromUser(string appUserId, int id)
        {
            bool isRemoved = this.carService.RemoveCar(appUserId, id);

            return isRemoved;
        }

        private int RunningDistanceToKm(int kms, int miles)
        {
            if (kms == 0 && miles == 0)
            {
                return 0;
            }

            if (kms == 0)
            {
                return (int)(OneMile * miles);
            }

            return kms;
        }

        private int EnginePowerToHp(int hp, int kw)
        {
            if (hp == 0 && kw == 0)
            {
                return 0;
            }

            if (hp == 0)
            {
                return (int) (OneKw * kw);
            }

            return hp;
        }
    }
}