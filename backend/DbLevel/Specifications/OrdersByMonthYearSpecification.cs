using DbLevel.Models;

namespace DbLevel.Specifications
{
    public class OrderbyDateRangeSpecification : SpecificationBase<Order>
    {
        public OrderbyDateRangeSpecification(DateTime start, DateTime end)
        {
            ApplyFilter(order => order.OrderDate >= start && order.OrderDate <= end);
        }
    }
}
