using AutoMapper;
using BussinessLogicLevel.Interfaces;
using DbLevel.Models;
<<<<<<< HEAD:ShopWebApi/Controllers/ProductController.cs
using FluentValidation;
=======
using Infrastucture.DtoModels;
using Microsoft.AspNetCore.Authorization;
>>>>>>> feature/addederrorhandlingservice:backend/ShopWebApi/Controllers/ProductController.cs
using Microsoft.AspNetCore.Mvc;

namespace ShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
<<<<<<< HEAD:ShopWebApi/Controllers/ProductController.cs
        private readonly IValidator<Product> _validator;
        public ProductController(IProductService productService, IValidator<Product> validator)
        {
            _productService = productService;
            _validator = validator;
=======
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
>>>>>>> feature/addederrorhandlingservice:backend/ShopWebApi/Controllers/ProductController.cs
        }

        [HttpGet("all")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync() 
        { 
            var item = await _productService.GetAllAsync();
            return Ok(item);
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
<<<<<<< HEAD:ShopWebApi/Controllers/ProductController.cs
            var result = await _validator.ValidateAsync(product);
            if (!result.IsValid)
            {
                return BadRequest(result.ToString());
            }
            
=======
            var product = _mapper.Map<Product>(productDto);   
>>>>>>> feature/addederrorhandlingservice:backend/ShopWebApi/Controllers/ProductController.cs
            await _productService.AddAsync(product);
            
            return Ok();
        }
        [HttpPut]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] ProductDto productDto)
        {
<<<<<<< HEAD:ShopWebApi/Controllers/ProductController.cs
            var result = await _validator.ValidateAsync(product);
            if (!result.IsValid)
            {
                return BadRequest(result.ToString());
            }
=======
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            var product = _mapper.Map<Product>(productDto);
>>>>>>> feature/addederrorhandlingservice:backend/ShopWebApi/Controllers/ProductController.cs
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
