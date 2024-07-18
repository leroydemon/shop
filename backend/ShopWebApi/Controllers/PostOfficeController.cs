using BussinessLogicLevel.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ShopWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostOfficeController : ControllerBase
    {
        private readonly IPostOfficeService _service;

        public PostOfficeController(IPostOfficeService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetPostOfficesJsonAsync()
            {
            return Ok(await _service.GetPostOfficesJsonAsync());
        }
    }
}
