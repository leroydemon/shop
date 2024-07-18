using DbLevel.Filter;
using DbLevel.SortableFields;

namespace DbLevel.Filters
{
    public class BrandFilter : FilterBase<BrandSortableFields>
    {
        public string? Name { get; set; }
        public string? Collection { get; set; }
        public string? Model { get; set; }
    }
}
