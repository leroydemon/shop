
using DbLevel.Interfaces;

namespace DbLevel.Models
{
    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDateTime { get; set; } = DateTime.UtcNow;
    }
}
