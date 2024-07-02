using DbLevel.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastucture.DtoModels
{
    public class UserDto : IdentityUser
    {
        public string Id { get; set; }
        public Guid? CartId { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IEnumerable<Product>? FavoriteList { get; set; }
        public IEnumerable<Order>? HistoryOrders { get; set; }
        public bool IsOnline { get; set; }
    }
}