using DbLevel.Interfaces;

namespace DbLevel.Models
{
    public class Order : EntityBase
    {
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string? ProductListJson { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}