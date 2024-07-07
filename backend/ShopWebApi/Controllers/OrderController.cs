using BussinessLogicLevel.Interfaces;
using DbLevel.Filters;
using DbLevel.Models;
using Microsoft.AspNetCore.Mvc;

namespace ShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;

        [HttpGet]
        public async Task<IActionResult> SearchAsync([FromQuery] OrderFilter filter)
        {
            var items = await _orderService.SearchAsync(filter);

            return Ok(items);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var item = await _orderService.GetByIdAsync(id);

            return Ok(item);
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
        public async Task<IActionResult> UpdateAsync(OrderDto order)
        {
            var updatedOrder = await _orderService.UpdateAsync(order);

            return Ok(updatedOrder);
        }
    }
}

