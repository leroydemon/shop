
namespace DbLevel.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid ProductStorageId { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<Product> ProductList { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
