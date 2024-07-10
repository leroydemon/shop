using BussinessLogicLevel.DtoModels;
using Microsoft.AspNetCore.Mvc;
using BussinessLogicLevel.Interfaces;

namespace ShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAccountService accountService) : ControllerBase
    {
        private readonly IAccountService _accountService = accountService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            var result = await _accountService.Register(request);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto input)
        {
            var token = await _accountService.Login(input);
            return Ok(token);

        }
        [HttpGet("logout")]
        public async Task<IActionResult> LogOut(Guid userId)
        {
            var result = await _accountService.LogOut(userId);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}