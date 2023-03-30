using app.Data.Models;

namespace app.Data.Interfaces
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        Task<bool> IsUserExist(string username);
        Task<Account?> GetWithRoleById(int id, bool asTracking = true);
        Task<Account?> GetWithRoleByName(string name, bool asTracking = true);
        Task<Account?> GetByName(string name, bool asTracking = true);
        Task<Account?> GetById(int id, bool asTracking = true);
    }
}