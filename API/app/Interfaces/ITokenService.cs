using app.Models.DTOs;

namespace app.Interfaces
{
    public interface ITokenService
    {
        Task<JwtDTO> Generate(int accountId);
        Task<JwtDTO> Refresh(string refreshToken);
        Task SaveNewRefreshToken(int accountId, string refreshToken);
        Task Revoke(string refreshToken);
    }
}