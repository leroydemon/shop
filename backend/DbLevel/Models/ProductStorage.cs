using DbLevel.Interfaces;

namespace DbLevel.Models
{
    public class ProductStorage : IBase
    {
        public Guid Id { get; set; }
        public Guid StorageId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }
}
