namespace DbLevel.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
       // public Guid ProductStorageId { get; set; }  per product
        public Guid UserId { get; set; }
        public IEnumerable<Product> ProductList { get; set; }
       // public decimal UnitPrice { get; set; } per product
        public decimal TotalPrice { get; set; }
        public int ProductAmount { get; set; }
    }
}
