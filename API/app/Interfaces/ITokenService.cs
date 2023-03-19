using app.Models.DTOs;

namespace app.Interfaces
{
    public interface ITokenService
    {
        Task<JwtDTO> GenerateAsync(int accountId);
        Task<JwtDTO> RefreshAsync(string refreshToken);
        Task SaveNewRefreshToken(int accountId, string refreshToken);
        Task Revoke(int accountId,string refreshToken);
    }
}