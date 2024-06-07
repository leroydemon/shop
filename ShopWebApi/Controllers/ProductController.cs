using BussinessLogicLevel.Interfaces;
using BussinessLogicLevel.Services;
using DbLevel.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IValidator<Product> _validator;
        public ProductController(IProductService productService, IValidator<Product> validator)
        {
            _productService = productService;
            _validator = validator;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync() 
        { 
            var item = await _productService.GetAllAsync();
            return Ok(item);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductByIdAsync(int id) 
        {
            var item = await _productService.GetProductByIdAsync(id);
            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(Product product)
        {
            var result = await _validator.ValidateAsync(product);
            if (!result.IsValid)
            {
                return BadRequest(result.ToString());
            }
            
            await _productService.AddAsync(product);
            
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] Product product)
        {
            var result = await _validator.ValidateAsync(product);
            if (!result.IsValid)
            {
                return BadRequest(result.ToString());
            }
            await _productService.UpdateAsync(id, product);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _productService.DeleteAsync(id);
            return Ok();
        }
    }
}
