using AutoMapper;
using BussinessLogicLevel.Interfaces;
using DbLevel.Interfaces;
using DbLevel.Models;

namespace BussinessLogicLevel.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IRepository<Order> orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderDto> AddAsync(OrderDto orderDto)
        {
            var addedOrder = await _orderRepository.AddAsync(_mapper.Map<Order>(orderDto));
            return _mapper.Map<OrderDto>(addedOrder);
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task RemoveAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            await _orderRepository.DeleteAsync(order);
        }

        public async Task<OrderDto> UpdateAsync(OrderDto order)
        {
            var updatedOrder = await _orderRepository.UpdateAsync(_mapper.Map<Order>(order));
            return _mapper.Map<OrderDto>(updatedOrder);
        }
    }
}
