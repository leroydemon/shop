
namespace DbLevel.Models
{
    public class Storage
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
