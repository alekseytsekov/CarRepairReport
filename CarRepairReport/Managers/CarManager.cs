namespace CarRepairReport.Managers
{
    using CarRepairReport.Managers.Interfaces;
    using CarRepairReport.Models.BindingModels;
    using CarRepairReport.Models.Models.CarComponents;
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