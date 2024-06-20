using BussinessLogicLevel.Interfaces;
using BussinessLogicLevel.Requests;
using DbLevel.Models;
using Infrastucture.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly TokenGeneratorService _tokenGenerator;
        private readonly IUserService _userService;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger, TokenGeneratorService tokenGenerator, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _tokenGenerator = tokenGenerator;
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var user = new User { UserName = request.Email, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return Ok("You registered successfully");
            }
            return BadRequest(result.Errors);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest input)
        {
            var result = await _signInManager.PasswordSignInAsync(input.Email, input.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(input.Email);
                var token = _tokenGenerator.GenerateJwtToken(user);
                await _userService.SetUserOnlineAsync(user.Id);
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }
        [HttpGet("logout")]
        public async Task<IActionResult> LogOut(string userId)
        {
            var user = await _userService.GetByIdAsync(userId);
            await _signInManager.SignOutAsync();
            if (_signInManager.SignOutAsync().IsCompleted)
            {
                await _userService.SetUserOfflineAsync(user.Id);
                _logger.LogInformation("User logged out");
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}