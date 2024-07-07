using DbLevel.Interfaces;

namespace DbLevel.Models
{
    public class Brand : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Collection { get; set; }
        public string Model { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime? UpdatedDateTime { get; set; } = DateTime.Now;
    }
}
