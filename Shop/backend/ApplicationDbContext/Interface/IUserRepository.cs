using DbLevel.Models;

namespace DbLevel.Interface
{
    public interface IUserRepository
    {
        Task<User> Add(User user);
        Task<User> GetByEmail(string email);
    }
}