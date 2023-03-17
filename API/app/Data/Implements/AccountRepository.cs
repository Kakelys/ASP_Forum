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

        public async Task<Account?> GetWithRoleById(int id, bool asTracking = true) =>
            await FindByCondition(x => x.Id == id, asTracking)
            .Include(x => x.Role)
            .FirstOrDefaultAsync();

        public async Task<bool> IsUserExist(string username)
        {
            var user = await FindByCondition(x => x.Username == username, false).FirstOrDefaultAsync();    

            return user != null;
        }
    }
}