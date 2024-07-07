using DbLevel.Filters;
using DbLevel.Models;
using DbLevel.SortableFields;

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
            switch (sortBy)
            {
                case OrderSortableFields.CartId:
                    if (ascending)
                        ApplyOrderBy(o => o.CartId);
                    else
                        ApplyOrderByDescending(o => o.CartId);
                    break;
                case OrderSortableFields.OrderTime:
                    if (ascending)
                        ApplyOrderBy(o => o.OrderDate);
                    else
                        ApplyOrderByDescending(o => o.OrderDate);
                    break;
                default:
                    if (ascending)
                        ApplyOrderBy(o => o.Id);
                    else
                        ApplyOrderByDescending(o => o.Id);
                    break;
            }
        }
    }
}