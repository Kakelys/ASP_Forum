using app.Interfaces;
using app.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/v1/tokens")]
    public class TokenController: BaseApiController
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(JwtDTO jwtDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _tokenService.RefreshAsync(jwtDto.RefreshToken));
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> Revoke(JwtDTO jwtDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await _tokenService.RevokeAsync(jwtDto.RefreshToken);
            return NoContent();
        }
    }
}