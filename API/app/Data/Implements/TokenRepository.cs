using app.Data.Interfaces;
using app.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Data.Implements
{
    public class TokenRepository : RepositoryBase<Token>, ITokenRepository
    {
        public TokenRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<Token?> GetToken(int accountId, string refreshToken, bool asTracking = true)
        {
            return await 
                FindByCondition(t => t.AccountId == accountId && t.TokenStr == refreshToken, asTracking)
                .FirstOrDefaultAsync();
        }

        public async Task<Token?> GetToken(int accountId, bool asTracking = true)
        {
            return await 
                FindByCondition(t => t.AccountId == accountId, asTracking)
                .FirstOrDefaultAsync();
        }
    }
}