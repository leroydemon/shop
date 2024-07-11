using DbLevel.Filter;
using DbLevel.SortableFields;

namespace DbLevel.Filters
{
    public class StorageFilter : FilterBase<StorageSortableFields>
    {
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
    }
}
