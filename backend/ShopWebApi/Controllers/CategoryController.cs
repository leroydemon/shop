using BussinessLogicLevel.Interfaces;
using DbLevel.Models;
using Microsoft.AspNetCore.Mvc;

namespace ShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;
        private readonly ILogger<CategoryController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            _logger.LogInformation("Я работаю!"); // не ври
            var item = await _categoryService.GetAllAsync();

            return Ok(item);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync(Guid id)
        {
            var item = await _categoryService.GetCategoryByIdAsync(id);

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CategoryDto category)
        {
            await _categoryService.AddAsync(category);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CategoryDto category)
        {
            var updatedCategory = await _categoryService.UpdateAsync(category);
            return Ok(updatedCategory);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
             await _categoryService.DeleteAsync(id);

             return Ok();
        }
    }
}
