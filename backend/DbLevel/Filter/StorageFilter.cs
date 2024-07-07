using DbLevel.SortableFields;

namespace DbLevel.Filters
{
    public class StorageFilter
    {
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public StorageSortableFields SortBy { get; set; }
        public bool Ascending {  get; set; } = true;
        public int Skip {  get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}
