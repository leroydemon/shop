
namespace DbLevel.Models
{
    public class PostOffice : EntityBase
    {
        public string Name { get; set; }
        public AddressInfo AddressInfo { get; set; }
        public string PostalCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
