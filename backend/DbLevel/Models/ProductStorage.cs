using DbLevel.Interfaces;

namespace DbLevel.Models
{
    public class ProductStorage : EntityBase
    {
        public Guid StorageId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
