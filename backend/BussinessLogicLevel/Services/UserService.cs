using BussinessLogicLevel.Interfaces;
using DbLevel.Interface;
using DbLevel.Models;

namespace BussinessLogicLevel.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<User>> GetSortedUsersAsync(string searchTerm, int pageNumber, int pageSize, string sortBy, bool ascending)
        {
           return await _userRepository.GetSortedUsersAsync(searchTerm, pageNumber, pageSize, sortBy, ascending);
        }

        public async Task SetUserOnlineAsync(string userId)
        {
            var user = await _userRepository.GetById(userId);
            user.IsOnline = true;
            await _userRepository.SaveChangesAsync();
        }
        public async Task SetUserOfflineAsync(string userId)
        {
            var user = await _userRepository.GetById(userId);
            user.IsOnline = false;
            await _userRepository.SaveChangesAsync();
        }

        public async Task RemoveAsync(string userId)
        {
            var user = await _userRepository.GetById(userId);
            await _userRepository.RemoveAsync(user);
        }
        public async Task<User> GetByIdAsync(string userId)
        {
            return await _userRepository.GetById(userId);
        }
    }
}

