using BussinessLogicLevel.Interfaces;
using BussinessLogicLevel.Services;
using DbLevel.Models;
using Microsoft.AspNetCore.Mvc;

namespace ShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("all")]
        public IActionResult GetAll() 
        { 
            var item = _productService.GetAll();
            return Ok(item);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetProductById(int id) 
        {
            var item = _productService.GetProductById(id);
            return Ok(item);
        }
        [HttpPost]
        public IActionResult Add(Product product)
        {
            _productService.Add(product);
            
            return Ok();
        }
        [HttpPut]
        public IActionResult Update(int id, [FromForm] Product product)
        {
            _productService.Update(id, product);
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id);
            return Ok();
        }
    }
}
