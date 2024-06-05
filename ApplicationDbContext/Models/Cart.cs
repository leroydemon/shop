
namespace DbLevel.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProductStorageId { get; set; }
        public int UserId { get; set; }
        public List<Product> ProductList { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductAmount { get; set; }
    }
}
