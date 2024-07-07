using DbLevel.Interfaces;

namespace DbLevel.Models
{
    public class ProductStorage : IEntity
    {
        public Guid Id { get; set; }
        public Guid StorageId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;   
        public DateTime? UpdatedDateTime { get; set; } = DateTime.Now;
    }
}
