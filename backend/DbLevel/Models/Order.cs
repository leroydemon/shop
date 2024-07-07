using DbLevel.Interfaces;

namespace DbLevel.Models
{
    public class Order : IEntity
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now; 
        public User User { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime? UpdatedDateTime { get; set; } = DateTime.Now;
    }
}