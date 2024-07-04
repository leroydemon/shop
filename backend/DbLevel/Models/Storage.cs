using DbLevel.Interfaces;

namespace DbLevel.Models
{
    public class Storage : IBase
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public ICollection<ProductStorage>? ProductStorage { get; set; }
    }
}
