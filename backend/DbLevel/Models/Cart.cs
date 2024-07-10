
using DbLevel.Interfaces;

namespace DbLevel.Models
{
    public class Cart : IBase
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? ProductListJson { get; set; }
        public Dictionary<Guid, int> ProductList { get; set; } = new Dictionary<Guid, int>();
        public decimal TotalPrice { get; set; }
        public int ProductAmount { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }
}
