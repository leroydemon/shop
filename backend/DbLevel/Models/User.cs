using DbLevel.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DbLevel.Models
{
    public class User : IdentityUser<Guid>, IBase
    {
        public Guid? CartId { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IEnumerable<Product>? FavoriteList { get; set; }
        public IEnumerable<Order>? HistoryOrders { get; set; }
        public bool IsOnline { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }
}
