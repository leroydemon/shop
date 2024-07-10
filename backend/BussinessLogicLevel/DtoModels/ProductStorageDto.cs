namespace DbLevel.Models
{
    public class ProductStorageDto
    {
        public Guid Id { get; set; }
        public Guid StorageId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
