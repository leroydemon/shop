using DbLevel.SortableFields;

namespace DbLevel.Filters
{
    public class OrderFilter
    {
        public Guid CartId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderSortableFields SortBy { get; set; }
        public bool Ascending {  get; set; } = true;
        public int Skip {  get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}
