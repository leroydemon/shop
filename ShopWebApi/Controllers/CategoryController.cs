using BussinessLogicLevel.Interfaces;
using BussinessLogicLevel.Services;
using DbLevel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var item = _categoryService.GetAll();
            return Ok(item);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetProductById(int id)
        {
            var item = _categoryService.GetCategoryById(id);
            return Ok(item);
        }
        [HttpPost]
        public IActionResult Add(Category category)
        {
            _categoryService.Add(category);
            return Ok();
        }
        [HttpPut]
        public IActionResult Update(int id, [FromForm] Category category)
        {
            _categoryService.Update(id, category);
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);
            return Ok();
        }
    }
}
