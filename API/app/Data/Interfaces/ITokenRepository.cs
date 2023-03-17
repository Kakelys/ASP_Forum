using app.Data.Models;

namespace app.Data.Interfaces
{
    public interface ITokenRepository : IRepositoryBase<Token>
    {
        Task<Token?> GetToken(int accountId, string refreshToken, bool asTracking = true);
        Task<Token?> GetToken(int accountId, bool asTracking = true);
        
    }
}