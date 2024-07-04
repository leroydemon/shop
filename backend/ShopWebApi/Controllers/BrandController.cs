using BussinessLogicLevel.Interfaces;
using DbLevel.Models;
using Microsoft.AspNetCore.Mvc;

namespace ShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController(IBrandService brandService) : ControllerBase
    {
        private readonly IBrandService _brandService = brandService;

        // создать фильтр по которому будет фильтрация айтемов
        // нельзя доставать все сразу
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var brands = await _brandService.GetAllAsync();
            return Ok(brands);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var brand = await _brandService.GetByIdAsync(id);
            return Ok(brand);
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(BrandDto brand)
        {
            await _brandService.AddAsync(brand);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            await _brandService.RemoveAsync(id);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(BrandDto brand)
        {
            var updatedBrand = await _brandService.UpdateAsync(brand);
            return Ok(updatedBrand);
        }
    }
}
