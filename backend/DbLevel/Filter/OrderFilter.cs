using DbLevel.Filter;
using DbLevel.SortableFields;

namespace DbLevel.Filters
{
    public class OrderFilter : FilterBase<OrderSortableFields>
    {
        public Guid? CartId { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}
