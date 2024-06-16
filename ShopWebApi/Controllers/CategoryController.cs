using BussinessLogicLevel.Interfaces;
using BussinessLogicLevel.Services;
using DbLevel.Models;
<<<<<<< HEAD
using FluentValidation;
using FluentValidation.AspNetCore;
=======
using Microsoft.AspNetCore.Authorization;
>>>>>>> feature/validationfixed
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IValidator<Category> _validator;
        public CategoryController(ICategoryService categoryService, IValidator<Category> validator)
        {
            _categoryService = categoryService;
            _validator = validator;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
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
        public async Task<IActionResult> AddAsync(Category category)
        {
            var result = await _validator.ValidateAsync(category);

            if(!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return BadRequest(result.ToString());
            }
            await _categoryService.AddAsync(category);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] Category category)
        {
            var result = await _validator.ValidateAsync(category);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return BadRequest(result.ToString());
            }
            await _categoryService.UpdateAsync(id, category);
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
             await _categoryService.DeleteAsync(id);
             return Ok();
        }
    }
}
