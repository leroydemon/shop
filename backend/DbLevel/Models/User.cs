using DbLevel.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DbLevel.Models
{
    public class User : IdentityUser<Guid>, IEntity
    {
        public Guid? CartId { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public AddressInfo AddressInfo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IEnumerable<Product>? FavoriteList { get; set; }
        public IEnumerable<Order>? HistoryOrders { get; set; }
        public bool IsOnline { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDateTime { get; set; } = DateTime.UtcNow;
    }
}
