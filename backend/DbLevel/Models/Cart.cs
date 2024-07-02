
namespace DbLevel.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? ProductListJson { get; set; }
        public Dictionary<Guid, int> ProductList { get; set; } = new Dictionary<Guid, int>();
        public decimal TotalPrice { get; set; }
        public int ProductAmount { get; set; }
    }
}
