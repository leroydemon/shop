using AutoMapper;
using BussinessLogicLevel.Interfaces;
using DbLevel.Filters;
using DbLevel.Interfaces;
using DbLevel.Models;
using DbLevel.Specifications;
using System.Text;

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

        public async Task<IEnumerable<OrderDto>> SearchAsync(OrderFilter filter)
        {
            var spec = new OrderSpecification(filter);
            var orders = await _orderRepository.ListAsync(spec);

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
        public async Task<byte[]> ExportOrdersToCsv(DateTime startDate, DateTime endDate)
        {
            var orders = await GetOrdersByDateRangeAsync(startDate, endDate);

            var csv = new StringBuilder();
            csv.AppendLine("Id,CustomerName,OrderDate,Amount");

            foreach (var order in orders)
            {
                csv.AppendLine($"{order.Id},{order.User.UserName},{order.User.Email},{order.OrderDate:yyyy-MM-dd},{order.TotalPrice}");
            }

            return Encoding.UTF8.GetBytes(csv.ToString());
        }

        private async Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var spec = new OrderbyDateRangeSpecification(startDate, endDate);

            return await _orderRepository.ListAsync(spec);
        }
    }
}
