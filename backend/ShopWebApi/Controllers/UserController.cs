using BussinessLogicLevel.Interfaces;
using DbLevel.Filters;
using DbLevel.Models;
using Microsoft.AspNetCore.Mvc;

namespace ShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<User>>> SearchAsync([FromQuery] UserFilter filter)
        {
            var users = await _userService.SearchAsync(filter);

            return Ok(users);
        }
        [HttpPatch("{userId}/offline")]
        public async Task<IActionResult> SetOfflineAsync(Guid userId)
        {
            await _userService.SetOfflineAsync(userId);

            return Ok();
        }
        [HttpPatch("{userId}/online")]
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);

            return Ok(user);
        }
    }
}
