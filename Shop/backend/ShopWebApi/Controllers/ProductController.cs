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

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync() 
        { 
            var items = await _productService.GetAllAsync();

            return Ok(items);
        }

        [HttpGet("{id:int}")]
        //[Authorize(Roles = "Admin")]    
        public async Task<IActionResult> GetProductByIdAsync(int id) 
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            var item = await _productService.GetProductByIdAsync(id);

            return Ok(item);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);   
            await _productService.AddAsync(product);
            
            return Ok();
        }

        [HttpPut]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] ProductDto productDto)
        {
            // 1. For what?
            // 2. In this controller you have such a validation, in other - nope

            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            var product = _mapper.Map<Product>(productDto);
            await _productService.UpdateAsync(id, product);

            return Ok();
        }

        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            await _productService.DeleteAsync(id);

            return Ok();
        }
    }
}
