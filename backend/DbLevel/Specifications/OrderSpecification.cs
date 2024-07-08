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
            ApplyFilter(o => o.CartId == filter.CartId);
            ApplyFilter(o => o.OrderDate == filter.OrderDate);

            ApplySorting(filter.SortBy, filter.Ascending);
            ApplyPaging(filter.Skip, filter.Take);
        }

        private void ApplySorting(OrderSortableFields sortBy, bool ascending)
        {
            Expression<Func<Order, object>> orderByExpression = sortBy switch
            {
                OrderSortableFields.CartId => o => o.CartId,
                OrderSortableFields.OrderTime => o => o.OrderDate,
                _ => o => o.Id
            };

            if (ascending)
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