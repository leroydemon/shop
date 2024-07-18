using BussinessLogicLevel.DtoModels;
using BussinessLogicLevel.Interfaces;
using DbLevel.Interfaces;
using DbLevel.Models;
using Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BussinessLogicLevel.Services
{
    // навести красоту с отступами и тд

    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountService> _logger;
        private readonly TokenGeneratorService _tokenGenerator;
        private readonly IRepository<User> _userRepo;
        private readonly IUserService _userService;
        private readonly IRepository<Cart> _cartRepository;
        public AccountService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<AccountService> logger,
            TokenGeneratorService tokenGenerator,
            IRepository<User> userRepository,
            IRepository<Cart> cartRepository,
            IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _tokenGenerator = tokenGenerator;
            _userRepo = userRepository;
            _cartRepository = cartRepository;
            _userService = userService;
        }
        public async Task<bool> Register(RegisterDto request)
        {
            var user = new User { UserName = request.Email, Email = request.Email, Latitude = 50.4501, Longitude = 30.5234 };
            var result = await _userManager.CreateAsync(user, request.Password);
            var cart = new Cart { UserId = user.Id };

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
            await _userService.SetOnlineAsync(user.Id);

            return token;
        }
        public async Task<bool> LogOut(Guid userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            await _signInManager.SignOutAsync();

            if (_signInManager.SignOutAsync().IsCompleted)
            {
                await _userService.SetOfflineAsync(userId);
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
