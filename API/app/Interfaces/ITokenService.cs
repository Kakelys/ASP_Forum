using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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