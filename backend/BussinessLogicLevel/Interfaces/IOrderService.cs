﻿using DbLevel.Models;
using System.Collections.Generic;

namespace BussinessLogicLevel.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto> AddAsync(OrderDto orderDto);
        Task UpdateAsync(Order orderDto);
        Task RemoveAsync(Guid id);
        Task<OrderDto> GetByIdAsync(Guid id);
    }
}
