using System.Linq.Expressions;

namespace app.Data.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool asTracking);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool asTracking);
        T Create(T entity);
        void CreateMany(IEnumerable<T> entity);
        void Delete(T entity);

    }
}