using app.Interfaces;
using app.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/v1/account")]
    public class AccountController : BaseApiController
    {
        private IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDTO registerDto)
        {
            if(registerDto == null 
            || string.IsNullOrEmpty(registerDto.Username) 
            || string.IsNullOrEmpty(registerDto.Password))
                return BadRequest("Username and password are required");

            return Ok(await _accountService.Register(registerDto));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginDTO loginDto)
        {
            if(loginDto == null 
            || string.IsNullOrEmpty(loginDto.Username) 
            || string.IsNullOrEmpty(loginDto.Password))
                return BadRequest("Username and password are required");

            return Ok(await _accountService.Login(loginDto));
        }

        
    }
}