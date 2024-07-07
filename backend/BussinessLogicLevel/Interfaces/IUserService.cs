using DbLevel.Filters;
using Infrastucture.DtoModels;

namespace BussinessLogicLevel.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> SearchAsync(UserFilter filter);
        Task SetOfflineAsync(Guid userId);
        Task SetOnlineAsync(Guid userId);
        Task RemoveAsync(Guid userId);
        Task<UserDto> GetByIdAsync(Guid userId);
        Task<UserDto> UpdateAsync(UserDto user);
        Task<UserDto> AddAsync(UserDto user);
    }
}