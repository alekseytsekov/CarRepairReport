namespace CarRepairReport.Services.Interfaces
{
    using System;
    using CarRepairReport.Data;
    using CarRepairReport.Models.Models.CommonModels;

    public abstract class Service : IService
    {
        protected ICarRepairReportData context;
        
        protected Service(ICarRepairReportData context)
        {
            this.context = context;
        }

        public bool Update()
        {
            try
            {
                this.context.Commit();
            }
            catch (Exception ex)
            {
                return this.LogError(ex);
            }

            return true;
        }

        protected bool LogError(Exception ex)
        {
            this.context.ErrorLogs.Add(new ErrorLog() { ErrorMessage = ex.Message , StackTrace = ex.StackTrace , CreatedOn = DateTime.UtcNow });
            this.context.Commit();

            return false;
        }

        public void LogError(string message, string controller)
        {
            this.context.ErrorLogs.Add(new ErrorLog() { ErrorMessage = message, StackTrace = controller, CreatedOn = DateTime.UtcNow});
            this.context.Commit();
        }
    }
}
