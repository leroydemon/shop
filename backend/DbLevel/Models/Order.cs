using DbLevel.Interfaces;

namespace DbLevel.Models
{
    public class Order : IEntity
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string CustomerName { get; set; }
        public string? ProductListJson { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime? UpdatedDateTime { get; set; } = DateTime.Now;
    }
}