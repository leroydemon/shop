
namespace DbLevel.Models
{
    public class User
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<Product> FavoriteList { get; set; } = new List<Product>();

    }
}
