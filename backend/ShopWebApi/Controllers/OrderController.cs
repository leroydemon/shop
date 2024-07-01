using BussinessLogicLevel.Interfaces;
using DbLevel.Models;
using Microsoft.AspNetCore.Mvc;

namespace ShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var brands = await _orderService.GetAllAsync();
            return Ok(brands);
        }
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var brand = await _orderService.GetByIdAsync(id);
            return Ok(brand);
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(OrderDto orderDto)
        {
            var item = await _orderService.AddAsync(orderDto);
            return Ok(item);
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            await _orderService.RemoveAsync(id);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Order order)
        {
            await _orderService.UpdateAsync(order);
            return Ok();
        }
    }
}

