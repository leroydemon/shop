using DbLevel.Models;

namespace DbLevel.Interface
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User> GetByEmail(string email);
        public Task<List<User>> GetSortedUsersAsync(string searchTerm, int pageNumber, int pageSize, string sortBy, bool ascending);
        public Task RemoveAsync(User user);
        public Task<User> GetById(string userId);
        public Task SaveChangesAsync();
    }
}