using app.Interfaces;
using app.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/v1/accounts")]
    public class AccountController : BaseApiController
    {
        private IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            return Ok(await _accountService.Register(registerDto));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            return Ok(await _accountService.Login(loginDto));
        }  
    }
}