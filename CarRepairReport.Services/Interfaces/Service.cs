namespace CarRepairReport.Services.Interfaces
{
    using CarRepairReport.Data;

    public abstract class Service
    {
        protected ICarRepairReportData context;

        //protected Service() : this(new CarRepairReportData())
        //{
            
        //}

        protected Service(ICarRepairReportData context)
        {
            this.context = context;
        }
    }
}
