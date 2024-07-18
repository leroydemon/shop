using DbLevel.Enum;
using DbLevel.Filters;
using DbLevel.Models;
using DbLevel.SortableFields;
using System.Linq.Expressions;

namespace DbLevel.Specifications
{
    public class OrderSpecification : SpecificationBase<Order>
    {
        public OrderSpecification(OrderFilter filter)
        {
            ApplyFilter(o => o.OrderDate == filter.OrderDate);

            ApplySorting(filter.OrderBy, filter.Ascending);
            ApplyPaging(filter.Skip, filter.Take);
        }

        private void ApplySorting(OrderSortableFields sortBy, OrderByDirection ascending)
        {
            Expression<Func<Order, object>> orderByExpression = sortBy switch
            {
                OrderSortableFields.OrderTime => o => o.OrderDate,
                _ => o => o.Id
            };

            if (ascending == OrderByDirection.Ascending)
            {
                ApplyOrderBy(orderByExpression);
            }
            else
            {
                ApplyOrderByDescending(orderByExpression);
            }
        }
    }
}