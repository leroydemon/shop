using DbLevel.Filter;
using DbLevel.SortableFields;

namespace DbLevel.Filters
{
    public class ProductStorageFilter : FilterBase<ProductStorageSortableFields>
    {
        public Guid? StorageId { get; set; }
        public Guid? ProductId { get; set; }
        public int? Quantity { get; set; }
    }
}
