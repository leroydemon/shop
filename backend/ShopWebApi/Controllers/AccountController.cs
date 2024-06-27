using BussinessLogicLevel.DtoModels;
using DbLevel.Interfaces;
using DbLevel.Models;
using Аuthorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BussinessLogicLevel.Services;
using BussinessLogicLevel.Interfaces;

namespace ShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
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
        [HttpPost("Login")]
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