using DbLevel.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbLevel.Models
{
    public class Cart : EntityBase
    {
        public Guid UserId { get; set; }
        public string? ProductListJson { get; set; }
        [NotMapped]
        public Dictionary<Guid, int> ProductList { get; set; } = new Dictionary<Guid, int>();
        public decimal TotalPrice { get; set; }
        public int ProductAmount { get; set; }
    }
}
