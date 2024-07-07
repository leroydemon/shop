using BussinessLogicLevel.Interfaces;
using DbLevel.Filters;
using Infrastucture.DtoModels;
using Microsoft.AspNetCore.Mvc;

namespace ShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService, ILogger<ProductController> logger) : ControllerBase
    {
        private readonly IProductService _productService = productService;
        private readonly ILogger<ProductController> _logger = logger;

        [HttpGet("search")]
        public async Task<IActionResult> SearchAsync([FromQuery] ProductFilter filter)
        {
            var items = await _productService.SearchAsync(filter);

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync(Guid id) 
        {
            var item = await _productService.GetByIdAsync(id);
            _logger.LogInformation("method works correctly");

            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] ProductDto productDto)
        {  
            var product = await _productService.AddAsync(productDto);
            _logger.LogInformation("method works correctly");

            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ProductDto product)
        {
            var updatedProduct = await _productService.UpdateAsync(product);
            _logger.LogInformation("method works correctly");

            return Ok(updatedProduct);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            await _productService.DeleteAsync(id);
            _logger.LogInformation("method works correctly");

            return Ok();
        }
    }
}
