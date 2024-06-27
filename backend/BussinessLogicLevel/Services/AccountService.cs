using BussinessLogicLevel.DtoModels;
using BussinessLogicLevel.Interfaces;
using DbLevel.Interfaces;
using DbLevel.Models;
using Аuthorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BussinessLogicLevel.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountService> _logger;
        private readonly TokenGeneratorService _tokenGenerator;
        private readonly IUserRepository _userRepo;
        private readonly IRepository<Cart> _cartRepository;
        public AccountService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<AccountService> logger,
            TokenGeneratorService tokenGenerator,
            IUserRepository userRepository,
            IRepository<Cart> cartRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _tokenGenerator = tokenGenerator;
            _userRepo = userRepository;
            _cartRepository = cartRepository;
        }
        public async Task<bool> Register(RegisterDto request)
        {
            var user = new User { UserName = request.Email, Email = request.Email};
            var result = await _userManager.CreateAsync(user, request.Password);
            var cart = new Cart { UserId = Guid.Parse(user.Id) };
            try
            {               
                 await _cartRepository.AddAsync(cart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            user.CartId = cart.Id;
            await _userRepo.UpdateAsync(user);
            if(result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<string> Login(LoginDto input)
        {
            var result = await _signInManager.PasswordSignInAsync(input.Email, input.Password, false, false);

            var user = await _userManager.FindByEmailAsync(input.Email);
            var token = _tokenGenerator.GenerateJwtToken(user);
            await _userRepo.SetOnlineAsync(user);
            return token;
        }
        public async Task<bool> LogOut(Guid userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            await _signInManager.SignOutAsync();
            if (_signInManager.SignOutAsync().IsCompleted)
            {
                await _userRepo.SetOfflineAsync(user);
                _logger.LogInformation("User logged out");
                return true;
            }
            else
            {
                _logger.LogInformation("Logging out failed");
                return false;
            }
        }
    }
}
