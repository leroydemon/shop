using BussinessLogicLevel.Interfaces;
using DbLevel.Models;
using Microsoft.AspNetCore.Mvc;

namespace ShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController(IStorageService StorageService, ILogger<StorageController> logger) : ControllerBase
    {
        private readonly IStorageService _storageService = StorageService;
        private readonly ILogger<StorageController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var item = await _storageService.GetAllAsync();
            _logger.LogInformation("method works correctly");
            return Ok(item);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync(Guid id)
        {
            var item = await _storageService.GetByIdAsync(id);
            _logger.LogInformation("method works correctly");
            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] StorageDto storageDto)
        {
            var product = await _storageService.AddAsync(storageDto);
            _logger.LogInformation("method works correctly");
            return Ok(product);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] StorageDto storage)
        {
            var updatedStorage = await _storageService.UpdateAsync(storage);
            _logger.LogInformation("method works correctly");
            return Ok(updatedStorage);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _storageService.DeleteAsync(id);
            _logger.LogInformation("method works correctly");
            return Ok();
        }
    }
}
