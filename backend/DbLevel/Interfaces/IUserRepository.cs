using DbLevel.Models;
using DbLevel.SortByEnum;

namespace DbLevel.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetSortedAsync(string searchTerm, int pageNumber, int pageSize, UserSortBy sortBy, bool ascending);
        Task<User> GetByEmailAsync(string email);
        public Task SetOfflineAsync(User user);
        public Task SetOnlineAsync(User user);
    }
}
