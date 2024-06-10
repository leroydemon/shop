using DbLevel.Models;

namespace DbLevel.Interface
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User> GetByEmail(string email);
    }
}