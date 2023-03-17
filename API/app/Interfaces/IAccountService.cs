using app.Models.DTOs;

namespace app.Interfaces
{
    public interface IAccountService
    {
        Task<UserDTO> Register(RegisterDTO registerDto);
        Task<UserDTO> Login(LoginDTO registerDto);
        
    }
}