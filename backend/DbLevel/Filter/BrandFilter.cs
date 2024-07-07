using DbLevel.SortableFields;

namespace DbLevel.Filters
{
    public class BrandFilter
    {
        public string Name { get; set; }
        public string? Collection { get; set; }
        public string Model { get; set; }
        public BrandSortableFields SortBy { get; set; }
        public bool Ascending {  get; set; } = true;
        public int Skip {  get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}
