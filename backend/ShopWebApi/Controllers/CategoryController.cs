using BussinessLogicLevel.Interfaces;
using DbLevel.Models;
using Microsoft.AspNetCore.Mvc;

namespace ShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            _logger.LogInformation("Я работаю!");
            var item = await _categoryService.GetAllAsync();

            return Ok(item);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            var item = await _categoryService.GetCategoryByIdAsync(id);

            return Ok(item);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAsync([FromBody] Category category)
        {
            await _categoryService.AddAsync(category);

            return Ok();
        }

        [HttpPut]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Category category)
        {
            await _categoryService.UpdateAsync(id, category);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
             await _categoryService.DeleteAsync(id);

             return Ok();
        }
    }
}
