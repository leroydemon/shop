
namespace DbLevel.Models
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? ProductListJson { get; set; }
        public Dictionary<Guid, int> ProductList { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductAmount { get; set; }
    }
}
