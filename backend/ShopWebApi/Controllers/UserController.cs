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
        [HttpGet("sortedlist")]
        public async Task<IActionResult> GetSortedAsync(string searchTerm, int pageNumber, int pageSize, string sortBy, bool ascending)
        {
             var items = await _userService.GetSortedAsync(searchTerm, pageNumber, pageSize, sortBy, ascending);
             return Ok(items);
        }
        [HttpGet("setoffline")]
        public async Task<IActionResult> SetOfflineAsync(Guid userId)
        {
            await _userService.SetOfflineAsync(userId);
            return Ok();
        }
        [HttpGet("setonline")]
        public async Task<IActionResult> SetOnlineAsync(Guid userId)
        {
            await _userService.SetOnlineAsync(userId);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid userId)
        {
            await _userService.RemoveAsync(userId);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(Guid userId)
        {
            var user = await _userService.GetByIdAsync(userId);
            return Ok(user);
        }
    }
}
