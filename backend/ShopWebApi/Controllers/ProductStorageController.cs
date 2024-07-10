using BussinessLogicLevel.Interfaces;
using DbLevel.Models;
using Microsoft.AspNetCore.Mvc;

namespace ShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductStorageController : ControllerBase
    {
        private readonly IProductStorageService _productStorageService;
        private readonly ILogger<ProductStorageController> _logger;
        public ProductStorageController(IProductStorageService productStorageService, ILogger<ProductStorageController> logger)
        {
            _productStorageService = productStorageService;
            _logger = logger;
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var item = await _productStorageService.GetAllAsync();
            _logger.LogInformation("method works correctly");
            return Ok(item);
        }
        [HttpGet("{id:int}")] 
        public async Task<IActionResult> GetProductByIdAsync(Guid id)
        {
            var item = await _productStorageService.GetByIdAsync(id);
            _logger.LogInformation("method works correctly");
            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] ProductStorageDto productDto)
        {
            var product = await _productStorageService.AddAsync(productDto);
            _logger.LogInformation("method works correctly");
            return Ok(product);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ProductStorageDto product)
        {
            var updatedPS = await _productStorageService.UpdateAsync(product);
            _logger.LogInformation("method works correctly");
            return Ok(updatedPS);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _productStorageService.DeleteAsync(id);
            _logger.LogInformation("method works correctly");
            return Ok();
        }
    }
}
