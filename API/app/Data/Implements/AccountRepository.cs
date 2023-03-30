using app.Data.Interfaces;
using app.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Data.Implements
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(RepositoryContext context) : base(context)
        {
        }

        public Task<Account?> GetByName(string name, bool asTracking = true) =>
            FindByCondition(x => x.Username == name, asTracking).FirstOrDefaultAsync();

        public Task<Account?> GetById(int id, bool asTracking = true) =>
            FindByCondition(x => x.Id == id, asTracking).FirstOrDefaultAsync();

        public async Task<Account?> GetWithRoleById(int id, bool asTracking = true) =>
            await FindByCondition(x => x.Id == id, asTracking)
            .Include(x => x.Role)
            .FirstOrDefaultAsync();

        public async Task<bool> IsUserExist(string username)
        {
            var user = await FindByCondition(x => x.Username == username, false).FirstOrDefaultAsync();    

            return user != null;
        }

        public async Task<Account?> GetWithRoleByName(string name, bool asTracking = true)
        {
            return await FindByCondition(x => x.Username == name, asTracking)
            .Include(x => x.Role)
            .FirstOrDefaultAsync();
        }
    }
}