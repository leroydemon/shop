using DbLevel.Interfaces;

namespace DbLevel.Models
{
    public class Storage : EntityBase
    {
        public string? Name { get; set; }
        public AddressInfo AddressInfo { get; set; }
        public string? Phone { get; set; }
        public ICollection<ProductStorage>? ProductStorage { get; set; }
    }
}
