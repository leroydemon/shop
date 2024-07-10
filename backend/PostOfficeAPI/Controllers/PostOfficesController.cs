using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace PostOfficeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostOfficesController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly PostOfficeService postOfficeService;

        public PostOfficesController(HttpClient httpClient, PostOfficeService postOfficeService)
        {
            _httpClient = httpClient;
            this.postOfficeService = postOfficeService;
        }
        [HttpGet]
        public async Task<string> GetPostOfficesAsync()
        {
            var stringjson = await postOfficeService.GetPostOfficesJsonAsync();
            return stringjson;
        }
    }
}
