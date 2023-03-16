using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using app.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace app.Data
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T: class
    {
        protected readonly RepositoryContext context;

        public RepositoryBase(RepositoryContext context)
        {
            this.context = context;
        }

        public T Insert(T entity) => context.Set<T>().Add(entity).Entity;

        public void InsertMany(IEnumerable<T> entities) =>
            context.AddRange(entities);

        public void Delete(T entity) => context.Set<T>().Remove(entity);

        public IQueryable<T> FindAll(bool asTracking) => 
            asTracking ? 
            context.Set<T>() :
            context.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool asTracking) =>
            asTracking ? 
                context.Set<T>().Where(expression) :
                context.Set<T>().Where(expression).AsNoTracking();
    }
}