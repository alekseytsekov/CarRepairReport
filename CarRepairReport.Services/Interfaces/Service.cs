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
            this.context.ErrorLogs.Add(new ErrorLog() { ErrorMessage = ex.Message, StackTrace = ex.StackTrace });
            this.context.Commit();

            return false;
        }
    }
}
