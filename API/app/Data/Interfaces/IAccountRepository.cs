using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Data.Models;

namespace app.Data.Interfaces
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        Task<bool> IsUserExist(string username);
        Task<Account?> GetWithRoleById(int id, bool asTracking);
        
        Task<Account?> GetByNameAsync(string name, bool asTracking = true);
        Task<Account?> GetByIdAsync(int id, bool asTracking = true);
    }
}