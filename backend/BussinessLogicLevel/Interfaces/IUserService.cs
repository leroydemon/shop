using DbLevel.Models;

namespace BussinessLogicLevel.Interfaces
{
    public interface IUserService
    {
        public Task<List<User>> GetSortedUsersAsync(string searchTerm, int pageNumber, int pageSize, string sortBy, bool ascending);
        public Task RemoveAsync(string userId);
        public Task<User> GetByIdAsync(string userId);
        public Task SetUserOfflineAsync(string userId);
        public Task SetUserOnlineAsync(string userId);
    }
}