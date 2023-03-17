namespace app.Data.Interfaces
{
    public interface IRepositoryManager
    {
        IRoleRepository Role { get; }
        IAccountRepository Account { get; }
        ISectionRepository Section { get; }
        IForumRepository Forum { get; }
        ITopicRepository Topic { get; }
        IPostRepository Post { get; }
        ITokenRepository Token { get; }

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