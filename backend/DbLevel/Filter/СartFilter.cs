using DbLevel.SortableFields;

namespace DbLevel.Filters
{
    public class CartFilter
    {
        public UserSortableFields SortBy { get; set; }
        public bool Ascending {  get; set; } = true;
        public int Skip {  get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}
