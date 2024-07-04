using DbLevel.Interfaces;

namespace DbLevel.Models
{
    public class Order : IBase
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public DateTime OrderDate { get; set; }
        public User User { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }
}