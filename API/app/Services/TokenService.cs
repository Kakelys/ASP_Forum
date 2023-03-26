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
        private const int REFRESH_TOKEN_EXPIRE_MINUTES = 60*24*30;  //30 days
        private const int MAX_TOKENS_PER_ACCOUNT = 5;
        private IConfiguration _config;
        private IRepositoryManager _repository;

        public TokenService(IConfiguration config, IRepositoryManager repostiroyManager)
        {
            _config = config;
            _repository = repostiroyManager;
        }

        public async Task<JwtDTO> Generate(int accountId)
        {
            //get user every time to get updated data(like role)
            var userForToken = await _repository.Account.GetWithRoleById(accountId, false);
            if(userForToken == null)
                throw new HttpResponseException(HttpStatusCode.InternalServerError, "Error while generating tokens");

            var accessToken = GenerateToken(userForToken, ACCESS_TOKEN_EXPIRE_MINUTES);
            var refreshToken = GenerateToken(userForToken, REFRESH_TOKEN_EXPIRE_MINUTES);

            var jwtDto = new JwtDTO(accessToken, refreshToken);
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
            await DeleteOldRefreshToken(accountId);
        }

        public async Task<JwtDTO> Refresh(string refreshToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(refreshToken);

            var accountId = int.Parse(jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

            var tokenEntity = await _repository.Token.GetToken(accountId, refreshToken);
            if(tokenEntity == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest, "Invalid refresh token");

            if(tokenEntity.ExpireDate < DateTime.Now)
                throw new HttpResponseException(HttpStatusCode.BadRequest, "Refresh token expired");

            var jwtDto = await Generate(accountId);

            tokenEntity.TokenStr = jwtDto.RefreshToken;
            tokenEntity.ExpireDate = DateTime.Now.AddMinutes(REFRESH_TOKEN_EXPIRE_MINUTES);

            await _repository.SaveAsync();

            return jwtDto;

            throw new NotImplementedException();
        }

        public async Task Revoke(string refreshToken)
        {
            var token = await _repository.Token.GetToken(refreshToken);
            if(token == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest, "Invalid refresh token");

            _repository.Token.Delete(token);
            await _repository.SaveAsync();
        }

        private async Task DeleteOldRefreshToken(int accountId)
        {
            var tokens = await _repository.Token.GetTokens(accountId);
            if(tokens.Count() >= MAX_TOKENS_PER_ACCOUNT)
            {
                var oldestToken = tokens.OrderBy(t => t.ExpireDate).First();
                _repository.Token.Delete(oldestToken);
                await _repository.SaveAsync();
            }
        }

        private string GenerateToken(Account account, int expireTimeMinutes)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Role, account.Role.Name)
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