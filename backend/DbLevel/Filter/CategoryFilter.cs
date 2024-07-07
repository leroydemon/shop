using DbLevel.SortableFields;

namespace DbLevel.Filters
{
    public class CategoryFilter
    {
        public string Name {  get; set; }
        public CategorySortableFields SortBy { get; set; }
        public bool Ascending {  get; set; } = true;
        public int Skip {  get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}
