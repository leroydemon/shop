using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace DbLevel.Models
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? ProductListJson { get; set; }
        [NotMapped]
        public Dictionary<Guid, int> ProductList { get; set; } = new Dictionary<Guid, int>();
        public decimal TotalPrice { get; set; }
        public int ProductAmount { get; set; }
    }
}
