using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using app.Data.Interfaces;
using app.Data.Models;
using app.Interfaces;
using app.Models.DTOs;
using app.Shared;
using Microsoft.IdentityModel.Tokens;

namespace app.Services
{
    public class TokenService : ITokenService
    {
        private const int ACCESS_TOKEN_EXPIRE_MINUTES = 30;
        private const int REFRESH_TOKEN_EXPIRE_MINUTES = 60*24*30;  
        private IConfiguration _config;
        private IRepositoryManager _repository;

        public TokenService(IConfiguration config, IRepositoryManager repostiroyManager)
        {
            _config = config;
            _repository = repostiroyManager;
        }
        public async Task<JwtDTO> GenerateAsync(int accountId)
        {
            //get user every time to get updated data(like role)
            var userForToken = await _repository.Account.GetWithRoleById(accountId, false);
            if(userForToken == null)
                throw new HttpResponseException(HttpStatusCode.InternalServerError, "Error while generating tokens");

            var accountDto = new AccountDTO(userForToken);

            var accessToken = GenerateToken(accountDto, ACCESS_TOKEN_EXPIRE_MINUTES);
            var refreshToken = GenerateToken(accountDto, REFRESH_TOKEN_EXPIRE_MINUTES);

            var jwtDto = new JwtDTO()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return jwtDto;
        }

        public async Task SaveNewRefreshToken(int accountId, string refreshToken)
        {

            var token = new Token()
            {
                AccountId = accountId,
                TokenStr = refreshToken,
                ExpireDate = DateTime.Now.AddMinutes(REFRESH_TOKEN_EXPIRE_MINUTES)
            };

            _repository.Token.Create(token);
            await _repository.SaveAsync();
        }

        public async Task<JwtDTO> RefreshAsync(string refreshToken)
        {
            //generate new access token
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(refreshToken);

            var accountId = int.Parse(jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

            var token = await _repository.Token.GetToken(accountId, refreshToken);
            if(token == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest, "Invalid refresh token");

            //get user with roles for generate tokens

            var jwtDto = await GenerateAsync(accountId);

            token.TokenStr = jwtDto.RefreshToken;
            token.ExpireDate = DateTime.Now.AddMinutes(REFRESH_TOKEN_EXPIRE_MINUTES);

            await _repository.SaveAsync();

            return jwtDto;

            throw new NotImplementedException();
        }

        public async Task Revoke(int userId, string refreshToken)
        {
            var token = await _repository.Token.GetToken(userId, refreshToken);
            if(token == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest, "Invalid refresh token");

            _repository.Token.Delete(token);
            await _repository.SaveAsync();
        }

        private string GenerateToken(AccountDTO accountDto, int expireTimeMinutes)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, accountDto.Username),
                new Claim(ClaimTypes.NameIdentifier, accountDto.Id.ToString()),
                new Claim(ClaimTypes.Role, accountDto.Role.Name)
            };

            //generate token
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(expireTimeMinutes),
                signingCredentials: signinCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            
            return token;
        }

        private bool IsTokenValid(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters()
                {
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidIssuer = _config["Jwt:Issuer"],
                    ValidAudience = _config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]))
                };

                SecurityToken validatedToken;
                IPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                return true;
            } 
            catch
            {
                return false;
            }
        }
    }
}