using System.Security.Claims;
using System.Net;
using app.Data.Interfaces;
using app.Interfaces;
using app.Models.DTOs;
using app.Shared;
using System.Security.Cryptography;
using app.Data.Models;
using System.IdentityModel.Tokens.Jwt;

namespace app.Services
{
    public class AccountService : IAccountService
    {   
        private IRepositoryManager _repository;
        private ITokenService _tokenService;

        public AccountService(IRepositoryManager repositoryManager, ITokenService tokenService)
        {
            _repository = repositoryManager;
            _tokenService = tokenService;
        }
        public async Task<UserDTO> Login(LoginDTO loginDto)
        {
            var user = await _repository.Account.GetByNameAsync(loginDto.Username);
            if(user == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest, "Username with such login doesn't exist");

            if(!CheckHash(loginDto.Password, user.PasswordHash))
                throw new HttpResponseException(HttpStatusCode.BadRequest, "Password is incorrect");

            var tokenDto = await _tokenService.Generate(user.Id);
            await _tokenService.SaveNewRefreshToken(user.Id, tokenDto.RefreshToken);

            return new UserDTO(user, tokenDto);
        }

        public async Task<UserDTO> Register(RegisterDTO registerDto)
        {
            //check if username exists
            if(await _repository.Account.IsUserExist(registerDto.Username))
                throw new HttpResponseException(HttpStatusCode.BadRequest, "Username already exists");
                
            //hash password
            var password_hash = HashPassword(registerDto.Password);
            var user = new Account()
            {
                Username = registerDto.Username,
                PasswordHash = password_hash
            };
            
            //save to db
            var newUser = _repository.Account.Create(user);
            await _repository.SaveAsync();

            //generate tokens
            var tokenDto = await _tokenService.Generate(user.Id);
            await _tokenService.SaveNewRefreshToken(user.Id, tokenDto.RefreshToken);

            //return UserDTO
            return new UserDTO(newUser, tokenDto);
        }

        public async Task<UserDTO> LoginWithToken(string refreshToken)
        {
            var jwt = await _tokenService.Refresh(refreshToken);

            var token = new JwtSecurityTokenHandler().ReadJwtToken(refreshToken);
            var userId = token.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var user = await _repository.Account.GetByIdAsync(int.Parse(userId));
            if(user == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest, "User doesn't exist");

            return new UserDTO(user, jwt);
        }

        private string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        private bool CheckHash(string password, string savedPasswordHash)
        {
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i=0; i < 20; i++)
                if (hashBytes[i+16] != hash[i])
                    return false;

            return true;
        }
    }
}