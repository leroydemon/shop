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
            var order = _mapper.Map<Order>(orderDto);
            var addedOrder = await _orderRepository.AddAsync(order);
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

        public async Task UpdateAsync(Order order)
        {
            var orderMapped = _mapper.Map<Order>(order);
            await _orderRepository.UpdateAsync(order);
        }
    }
}
