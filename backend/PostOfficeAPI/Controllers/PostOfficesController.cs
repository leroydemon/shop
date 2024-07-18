using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace PostOfficeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostOfficesController : ControllerBase
    {
        private readonly PostOfficeService _postOfficeService;

        public PostOfficesController(PostOfficeService postOfficeService)
        {
            _postOfficeService = postOfficeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPostOfficesAsync()
        {
            var stringjson = await _postOfficeService.GetPostOfficesAsync();
            return Ok(stringjson);
        }
    }
}
