using System.Linq.Expressions;

namespace app.Data.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool asTracking = true);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool asTracking = true);
        T Insert(T entity);
        void InsertMany(IEnumerable<T> entity);
        void Delete(T entity);

    }
}