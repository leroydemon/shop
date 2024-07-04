using DbLevel.Interfaces;

namespace DbLevel.Models
{
    public class Category : IBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }
}
