

using DbLevel.Enum;

namespace DbLevel.Models
{
    public  class Product
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public int StorageId { get; set; }
        public int BrandId { get; set; }
        public Size Size { get; set; }
        public Gender Gender { get; set; }
        public Season Season { get; set; }
        public Purpose Propose { get; set; }
        public string Name { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? Description { get; set; } = null;

    }
}
