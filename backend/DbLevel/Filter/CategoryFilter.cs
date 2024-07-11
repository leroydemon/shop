using DbLevel.Filter;
using DbLevel.SortableFields;

namespace DbLevel.Filters
{
    public class CategoryFilter : FilterBase<CategorySortableFields>
    {
        public string Name {  get; set; }
    }
}
