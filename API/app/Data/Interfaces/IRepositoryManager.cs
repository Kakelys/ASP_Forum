namespace app.Data.Interfaces
{
    public interface IRepositoryManager
    {
        void BeginTransaction();
        Task BeginTransactionAsync();
        void Commit();
        Task CommitAsync();
        void Rollback();
        Task RollbackAsync();
        void Save();
        Task SaveAsync();
    }
}