namespace CarRepairReport.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Linq.Expressions;
    public class EntityRepository<T> : IEntityRepository<T> where T: class 
    {
        private IDbSet<T> set;

        public EntityRepository(IDbSet<T> dbSet)
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

        public IQueryable<T> GetAllWithRemoved()
        {
            return this.set;
        }

        public T GetById(object id)
        {
            return this.set.Find(id);
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
