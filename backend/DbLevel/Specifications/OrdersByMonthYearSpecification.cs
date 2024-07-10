using DbLevel.Models;

namespace DbLevel.Specifications
{
    public class OrdersByMonthYearSpecification : SpecificationBase<Order>
    {
        public OrdersByMonthYearSpecification(int year, int month)
        {
            ApplyFilter(o => o.OrderDate.Month == month && o.OrderDate.Year == year);
        }
    }
}
