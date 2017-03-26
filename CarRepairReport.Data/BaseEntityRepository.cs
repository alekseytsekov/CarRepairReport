namespace CarRepairReport.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Linq.Expressions;
    using CarRepairReport.Models;
    public class BaseEntityRepository<T> : IBaseEntityRepository<T> where T:class, IBaseModel
    {
        private IDbSet<T> set;

        public BaseEntityRepository(IDbSet<T> dbSet)
        {
            this.set = dbSet;
        }

        public void Add(T entity)
        {
            this.set.Add(entity);
        }

        public void Add(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                this.Add(entity);
            }
        }

        public void Remove(T entity)
        {
            entity.IsDeleted = true;
            this.set.AddOrUpdate(entity);
        }

        public void Remove(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                this.Remove(entity);
            }
        }

        public void PermanentRemove(T entity)
        {
            this.set.Remove(entity);
        }

        public T FirstOrDefault()
        {
            return this.set.FirstOrDefault();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return this.set.FirstOrDefault(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return this.set.Where(x => !x.IsDeleted);
        }

        public IQueryable<T> GetAllWithRemoved()
        {
            return this.set;
        }

        public T GetById(object id)
        {
            return this.set.Find(id);
        }

        public IEnumerable<T> All()
        {
            return this.set.Where(x => !x.IsDeleted).ToArray();
        }

        public IEnumerable<T> AllWithRemoved()
        {
            return this.set.ToArray();
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return this.set.Where(predicate);
        }

        public void Update(T entity)
        {
            this.set.AddOrUpdate(entity);
        }

        public bool Any()
        {
            return this.set.Any();
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return this.set.Any(predicate);
        }
    }
}
