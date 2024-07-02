using AutoMapper;
using BussinessLogicLevel.Interfaces;
using DbLevel.Interfaces;
using DbLevel.Models;
using Infrastucture.DtoModels;

namespace BussinessLogicLevel.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserDto>> GetSortedAsync(string searchTerm, int pageNumber, int pageSize, string sortBy, bool ascending)
        {
            var users = await _userRepository.GetSortedAsync(searchTerm, pageNumber, pageSize, sortBy, ascending);
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task SetOnlineAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            await _userRepository.SetOnlineAsync(user);
        }
        public async Task SetOfflineAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            await _userRepository.SetOfflineAsync(user);
        }

        public async Task RemoveAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            await _userRepository.DeleteAsync(user);
        }
        public async Task<UserDto> GetByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return _mapper.Map<UserDto>(user);
        }
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> UpdateAsync(UserDto user)
        {
            var updatedUser = await _userRepository.UpdateAsync(_mapper.Map<User>(user));
            var userDto = new UserDto
            {
                Id = updatedUser.Id,
                UserName = updatedUser.UserName,
                Email = updatedUser.Email,
                NormalizedUserName = updatedUser.NormalizedUserName,
            };
            return userDto;
        }

        public async Task<UserDto> AddAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var addedUser = await _userRepository.AddAsync(user);
            return _mapper.Map<UserDto>(addedUser);
        }
    }
}

