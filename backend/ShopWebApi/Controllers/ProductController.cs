using BussinessLogicLevel.Interfaces;
using BussinessLogicLevel.Services;
using DbLevel.Models;
using DbLevel.SortByEnum;
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
        public async Task<IActionResult> GetSortedAsync(string searchTerm, int pageNumber, int pageSize, SortBy sortBy, bool ascending)
        {
            //var items = await _productService.GetSortedAsync(searchTerm, pageNumber, pageSize, sortBy, ascending);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync() 
        { 
            var item = await _productService.GetAllAsync();
            _logger.LogInformation("method works correctly");
            return Ok(item);
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
