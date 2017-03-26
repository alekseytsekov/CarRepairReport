namespace CarRepairReport.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using CarRepairReport.Models;
    public interface IBaseEntityRepository<T>: IEntityRepository<T> where T: class, IBaseModel
    {
        void Remove(T entity);
        void Remove(IEnumerable<T> entities);
        IQueryable<T> GetAll();
        IEnumerable<T> All();
    }
}
