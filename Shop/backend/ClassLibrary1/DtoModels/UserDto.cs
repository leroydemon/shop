using DbLevel.Models;

namespace Infrastucture.DtoModels
{
    public class UserDto
    {
        public Guid? CartId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IEnumerable<Product>? FavoriteList { get; set; }
    }
}