using DbLevel.Models;
using Infrastucture.DtoModels;

namespace BussinessLogicLevel.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetSortedAsync(string searchTerm, int pageNumber, int pageSize, string sortBy, bool ascending);
        Task SetOfflineAsync(Guid userId);
        Task SetOnlineAsync(Guid userId);
        Task RemoveAsync(Guid userId);
        Task<UserDto> GetByIdAsync(Guid userId);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task UpdateAsync(User user);
        Task<UserDto> AddAsync(UserDto user);
    }
}