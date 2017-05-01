namespace CarRepairReport.Services.Interfaces
{
    public interface IService
    {
        bool Update();
        void LogError(string message, string controller);
    }
}
