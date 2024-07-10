using DbLevel.Enum;
using DbLevel.Interfaces;

namespace DbLevel.Models
{
    public  class Product : IBase
    {
        public Guid Id { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public Size Size { get; set; }
        public Gender Gender { get; set; }
        public Season Season { get; set; }
        public Purpose Propose { get; set; }
        public string? Name { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Description { get; set; }
        public ICollection<ProductStorage> ProductStorage { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }
}
