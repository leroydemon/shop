using DbLevel.Enum;
using DbLevel.Filter;
using DbLevel.SortableFields;

namespace DbLevel.Filters
{
    public class ProductFilter : FilterBase<ProductSortableFields>
    {
        public Guid? CategoryId { get; set; }
        public Guid? BrandId { get; set; }
        public Size? Size { get; set; }
        public Gender? Gender { get; set; }
        public Season? Season { get; set; }
        public Purpose? Purpose { get; set; }
        public string? Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
