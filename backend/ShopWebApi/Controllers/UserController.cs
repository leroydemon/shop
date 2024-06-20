using BussinessLogicLevel.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetSortedUsers(string searchTerm, int pageNumber, int pageSize, string sortBy, bool ascending)
        {
             var items = await _userService.GetSortedUsersAsync(searchTerm, pageNumber, pageSize, sortBy, ascending);
             return Ok(items);
        }
        public async Task<IActionResult> SetUserOfflineAsync(string userId)
        {
            await _userService.SetUserOfflineAsync(userId);
            return Ok();
        }
        public async Task<IActionResult> SetUserOnlineAsync(string userId)
        {
            await _userService.SetUserOnlineAsync(userId);
            return Ok();
        }

        public async Task<IActionResult> RemoveAsync(string userId)
        {
            await _userService.RemoveAsync(userId);
            return Ok();
        }
        public async Task<IActionResult> GetByIdAsync(string userId)
        {
            await _userService.GetByIdAsync(userId);
            return Ok();
        }
    }
}
