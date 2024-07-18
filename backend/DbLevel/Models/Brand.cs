using DbLevel.Interfaces;

namespace DbLevel.Models
{
    public class Brand : EntityBase
    {
        public string Name { get; set; }
        public string? Collection { get; set; }
        public string Model { get; set; }
    }
}
