namespace CarRepairReport.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using CarRepairReport.Models;
    public interface IEntityRepository <T> where T: class 
    {
        void Add(T entity);
        void Add(IEnumerable<T> entities);
        void Update(T entity);
        bool Any();
        bool Any(Expression<Func<T, bool>> predicate);
        
        void PermanentRemove(T entity);
        T FirstOrDefault();
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        T GetById(object id);
        IQueryable<T> GetAllWithRemoved();
        IEnumerable<T> AllWithRemoved();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
    }
}
