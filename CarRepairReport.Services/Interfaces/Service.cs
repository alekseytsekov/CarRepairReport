namespace CarRepairReport.Services.Interfaces
{
    using System;
    using CarRepairReport.Data;

    public abstract class Service : IService
    {
        protected ICarRepairReportData context;

        //protected Service() : this(new CarRepairReportData())
        //{
            
        //}

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
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
