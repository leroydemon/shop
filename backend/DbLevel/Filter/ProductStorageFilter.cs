using DbLevel.SortableFields;

namespace DbLevel.Filters
{
    public class ProductStorageFilter
    {
        public Guid StorageId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public ProductStorageSortableFields SortBy { get; set; }
        public bool Ascending {  get; set; } = true;
        public int Skip {  get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}
