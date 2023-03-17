using app.Data.Interfaces;
using app.Data.Models;

namespace app.Data.Implements
{
    public class TokenRepository : RepositoryBase<Token>, ITokenRepository
    {
        public TokenRepository(RepositoryContext context) : base(context)
        {
        }
    }
}