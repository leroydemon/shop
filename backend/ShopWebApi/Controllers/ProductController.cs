using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;

        public ProductController(IProductService productService, IMapper mapper, ILogger<CategoryController> logger)
        {
            _productService = productService;
            _mapper = mapper;
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
        public async Task<IActionResult> GetProductByIdAsync(int id) 
        {
            var item = await _productService.GetProductByIdAsync(id);
            _logger.LogInformation("method works correctly");
            return Ok(item);
        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAsync([FromBody] ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);   
            await _productService.AddAsync(product);
            _logger.LogInformation("method works correctly");
            return Created();
        }
        [HttpPut]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync([FromBody] ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productService.UpdateAsync(product);
            _logger.LogInformation("method works correctly");
            return Ok();
        }
        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _productService.DeleteAsync(id);
            _logger.LogInformation("method works correctly");
            return Ok();
        }
    }
}
