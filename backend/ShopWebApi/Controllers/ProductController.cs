using BussinessLogicLevel.Interfaces;
using DbLevel.Models;
using Infrastucture.DtoModels;
using Microsoft.AspNetCore.Mvc;

namespace ShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
        [HttpGet("all")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync() 
        { 
            var item = await _productService.GetAllAsync();
            _logger.LogInformation("method works correctly");
            return Ok(item);
        }
        [HttpGet("{id:int}")]
        //[Authorize(Roles = "Admin")]    
        public async Task<IActionResult> GetProductByIdAsync(Guid id) 
        {
            var item = await _productService.GetByIdAsync(id);
            _logger.LogInformation("method works correctly");
            return Ok(item);
        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAsync([FromBody] ProductDto productDto)
        {  
            var product = await _productService.AddAsync(productDto);
            _logger.LogInformation("method works correctly");
            return Ok(product);
        }
        [HttpPut]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync([FromBody] Product product)
        {
            await _productService.UpdateAsync(product);
            _logger.LogInformation("method works correctly");
            return Ok();
        }
        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _productService.DeleteAsync(id);
            _logger.LogInformation("method works correctly");
            return Ok();
        }
    }
}
