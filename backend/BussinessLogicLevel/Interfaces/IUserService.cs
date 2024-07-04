using DbLevel.Interfaces;
using DbLevel.SortByEnum;
using Infrastucture.DtoModels;

namespace BussinessLogicLevel.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetSortedAsync(string searchTerm, int pageNumber, int pageSize, UserSortBy sortBy, bool ascending);
        Task SetOfflineAsync(Guid userId);
        Task SetOnlineAsync(Guid userId);
        Task RemoveAsync(Guid userId);
        Task<UserDto> GetByIdAsync(Guid userId);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> UpdateAsync(UserDto user);
        Task<UserDto> AddAsync(UserDto user);
    }
}