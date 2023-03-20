using app.Data.Models;

namespace app.Data.Interfaces
{
    public interface ITokenRepository : IRepositoryBase<Token>
    {
        Task<Token?> GetTokenAsync(int accountId, string refreshToken, bool asTracking = true);
        Task<Token?> GetTokenAsync(string refreshToken, bool asTracking = true);
        Task<Token?> GetTokenAsync(int accountId, bool asTracking = true);
        
    }
}